using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
	public class AppController : Controller
	{
		private IMailService _mailService;
		private IWorldRepository _worldRep;

		public AppController(IMailService service, IWorldRepository repo)
		{
			_mailService = service;
			_worldRep = repo;
		}

		public IActionResult Index()
		{
			return View();
		}

		[Authorize]
		public IActionResult Trips()
		{
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