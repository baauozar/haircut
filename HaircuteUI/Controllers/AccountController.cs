using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HaircuteUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;

		public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
	
		public IActionResult Login(string returnUrl = null)
		{
			// **Set default returnUrl to Admin/Faq/Index if not provided**
			ViewData["ReturnUrl"] = returnUrl ?? Url.Action("Index", "Faq", new { area = "Admin" });
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
	
		public async Task<IActionResult> Login(string email, string password)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

				if (result.Succeeded)
				{
					// Directly redirect to the Admin FAQ Index page
					return RedirectToAction("Index", "Faq", new { area = "Admin" });
				}

				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			}

			// Return to login view with validation errors
			return View();
		}



		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
