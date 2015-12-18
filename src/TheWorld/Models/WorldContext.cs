using Microsoft.Data.Entity;

namespace TheWorld.Models
{
	public class WorldContext : DbContext
	{
		public WorldContext()
		{
			Database.EnsureCreated();
		}

		public DbSet<Trip> Trips { get; set; }
		public DbSet<Stop> Stops { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionString = Startup.Configuration["Data:WorldContextConnection"];
			optionsBuilder.UseSqlServer(connectionString);
			base.OnConfiguring(optionsBuilder);
		}
	}
}