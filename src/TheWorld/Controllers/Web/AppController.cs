using Microsoft.AspNet.Mvc;
using System.Linq;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
	public class AppController : Controller
	{
		private IMailService _mailService;
		private WorldContext _context;

		public AppController(IMailService service, WorldContext context)
		{
			_mailService = service;
			_context = context;
		}

		public IActionResult Index()
		{
			var trips = _context.Trips.OrderBy(t => t.Name).ToList();
			return View();
		}
		
		public IActionResult About()
		{
			return View();
		}
		
		public IActionResult Contact()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Contact(ContactViewModel model)
		{
			if (ModelState.IsValid)
			{
				var siteEmail = Startup.Configuration["AppSettings:SiteEmailAddress"];

				if (string.IsNullOrWhiteSpace(siteEmail))
				{
					ModelState.AddModelError("", "Could not send email, configuration problem.");
				}

				if (_mailService.SendMail(siteEmail, siteEmail,
					$"Contact from {model.Name} ({model.Email})", model.Message))
				{
					ModelState.Clear();
					ViewBag.Message = "Mail Sent. Thanks!";
				}
			}

			return View();
		}
	}
}