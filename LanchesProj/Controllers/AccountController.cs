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

			if (user != null)
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

		//metodo get
		public IActionResult Register() {

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(LoginViewModel model) {
			if (ModelState.IsValid) {
				var user = new IdentityUser { UserName = model.Username };
				var result = await _userManager.CreateAsync(user, model.Password);

				if (model.Password != model.ConfirmPassword)
				{
					this.ModelState.AddModelError("Registro", "A senha não bate com a confirmação");
				}
				else
				{
					if (result.Succeeded)
					{
						//await _signInManager.SignInAsync(user, isPersistent: false); //podemos tentar fazer o sign in também
						return RedirectToAction("Login", "Account");
					}
					else
					{
						this.ModelState.AddModelError("Registro", "Falha ao registrar usuário");
					}
				}
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Logout() {
			HttpContext.Session.Clear();
			HttpContext.User = null;
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}


	}
}
