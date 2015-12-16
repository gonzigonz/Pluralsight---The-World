using Microsoft.AspNet.Mvc;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
	public class AppController : Controller
	{
		private IMailService _mailService;

		public AppController(IMailService service)
		{
			_mailService = service;
		}

		public IActionResult Index()
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
			_mailService.SendMail(
				"",
				"",
				$"Contact from {model.Name} ({model.Email})",
				model.Message);

			return View();
		}
	}
}