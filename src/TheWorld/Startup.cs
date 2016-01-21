using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld
{
	public class Startup
    {
		public static IConfiguration Configuration;

		public Startup(IApplicationEnvironment appEnv)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(appEnv.ApplicationBasePath)
				.AddJsonFile("config.json")
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc(config =>
				{
#if !DEBUG
					config.Filters.Add(new RequireHttpsAttribute());
#endif
				})
				.AddJsonOptions(opt =>
				{
					opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				});

			services.AddIdentity<WorldUser, IdentityRole>(config =>
			{
				config.User.RequireUniqueEmail = true;
				config.Password.RequiredLength = 6;
				config.Password.RequireNonLetterOrDigit = false;
				config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
				config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
				{
					OnRedirectToLogin = ctx =>
					{
						if (ctx.Request.Path.StartsWithSegments("/api") &&
						ctx.Response.StatusCode == (int)HttpStatusCode.OK)
						{
							ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
						}
						else
						{
							ctx.Response.Redirect(ctx.RedirectUri);
						}
						
						return Task.FromResult(0);
					}
				};
			})
			.AddEntityFrameworkStores<WorldContext>();

			services.AddLogging();

			services.AddEntityFramework()
				.AddSqlServer()
				.AddDbContext<WorldContext>();

			services.AddTransient<WorldContextSeedData>();
			services.AddScoped<IWorldRepository, WorldRepository>();
			services.AddScoped<CoordService>();

#if DEBUG
			services.AddScoped<IMailService, DebugMailService>();
#else
			services.AddScoped<IMailService, RealMailService>();
#endif

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public async void Configure(
			IApplicationBuilder app, 
			WorldContextSeedData seedData, 
			ILoggerFactory loggerFactory,
			IHostingEnvironment env)
        {
			if (env.IsDevelopment())
			{
				loggerFactory.AddDebug(LogLevel.Information);
				app.UseDeveloperExceptionPage();
			}
			else
			{
				loggerFactory.AddDebug(LogLevel.Error);
				app.UseExceptionHandler("/App/Error");
			}
			

			app.UseStaticFiles();

			app.UseIdentity();

			AutoMapper.Mapper.Initialize(config => 
			{
				config.CreateMap<Trip, TripViewModel>().ReverseMap();
				config.CreateMap<Stop, StopViewModel>().ReverseMap();
			});

			app.UseMvc(config =>
			{
				config.MapRoute(
					name: "Default",
					template: "{controller}/{action}/{id?}",
					defaults: new { controller = "App", action = "Index" }
					);
			});
			
			await seedData.EnsureSeedDataAsync();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
