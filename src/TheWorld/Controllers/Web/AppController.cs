using Microsoft.AspNet.Mvc;
using System;

namespace TheWorld.Controllers.Web
{
	public class AppController : Controller
	{
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
	}
}