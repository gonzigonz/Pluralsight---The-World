using Microsoft.AspNet.Mvc;

namespace TheWorld.Controllers
{
	public class AuthController: Controller
    {
		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Trips", "Apps");
			}

			return View();
		}
    }
}
