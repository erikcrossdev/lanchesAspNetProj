using LanchesProj.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchesProj.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		//igual ao HttpGet 
		public IActionResult Login(string returnUrl = null)
		{
			return View(new LoginViewModel() { 
				ReturnUrl = returnUrl 
			});
		}

		//post
		[HttpPost] //é obrigatório dizer que é post, diferente do get
		public async Task<IActionResult> Login(LoginViewModel model) {
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var user = await _userManager.FindByNameAsync(model.Username);

			if (user == null)
			{
				var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);//não persiste o cookie e não bloqueia ao falhar
				if (result.Succeeded)
				{
					if (string.IsNullOrEmpty(model.ReturnUrl))
					{
						return RedirectToAction("Index", "Home");
					}
					return Redirect(model.ReturnUrl); //vai para a url se não for nula
				}
			}
			ModelState.AddModelError("", "Falha ao realizar o login!");
			return View(model);
		}

	}
}
