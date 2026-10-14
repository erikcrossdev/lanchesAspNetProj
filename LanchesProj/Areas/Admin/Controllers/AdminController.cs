using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesProj.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize("Admin")] //precisa do perfil admin
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
