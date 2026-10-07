using Microsoft.AspNetCore.Mvc;

namespace LanchesProj.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
