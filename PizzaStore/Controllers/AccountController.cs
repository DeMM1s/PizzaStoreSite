using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Models.ViewModels;
using System.Threading.Tasks;


namespace PizzaStore.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private UserManager<IdentityUser> userManager;
		private SignInManager<IdentityUser> signInManager;

		public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
		{
			userManager = userMgr;
			signInManager = signInMgr;
		}

		[AllowAnonymous]
		public ViewResult Login(string returnUrl)
		{
			return View(new LoginViewModel
			{
				ReturnUrl = returnUrl
			});
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (ModelState.IsValid)
			{
				IdentityUser user = await userManager.FindByNameAsync(loginViewModel.Username);
				if (user != null)
				{
					await signInManager.SignOutAsync();
					if ((await signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false)).Succeeded)
					{
						return Redirect(loginViewModel?.ReturnUrl ?? "Admin/Index");
					}
				}
			}
			ModelState.AddModelError("", "Неправильное имя пользователя или пароль");
			return View(loginViewModel);
		}

		public async Task<RedirectResult> Logout(string returlUrl = "/")
		{
			await signInManager.SignOutAsync();
			return Redirect(returlUrl);
		}
	}
}
