using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Auth
{
	public class AuthController: Controller
    {
		private SignInManager<WorldUser> _singInManager;

		public AuthController(SignInManager<WorldUser> signInManager)
		{
			_singInManager = signInManager;
		}

		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Trips", "Apps");
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(string returnUrl, LoginViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var signInResults = await _singInManager.PasswordSignInAsync(
												vm.Username, vm.Password, true, false
												);

				if (signInResults.Succeeded)
				{
					if (string.IsNullOrWhiteSpace(returnUrl))
					{
						return RedirectToAction("Trips", "App");
					}
					else
					{
						return Redirect(returnUrl);
					}
					
				}
				else
				{
					ModelState.AddModelError("", "Username or password is incorrect");
				}
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			if (User.Identity.IsAuthenticated)
			{
				await _singInManager.SignOutAsync();
			}

			return RedirectToAction("Index", "App");
		}
    }
}
