using LanchesProj.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchesProj.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminGraficoController : Controller
	{
		private readonly GraficoVendasService _graficoVendasService;

		public AdminGraficoController(GraficoVendasService graficoVendas) {
			_graficoVendasService = graficoVendas ?? throw
				new ArgumentNullException(nameof(graficoVendas));
		}

		public JsonResult VendasLanches(int dias) {
			var lanchesVendasTotais = _graficoVendasService.GetVendasLanches(dias);

			return Json(lanchesVendasTotais);
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult VendasMensal()
		{
			return View();
		}

		[HttpGet]
		public IActionResult VendasSemanal()
		{
			return View();
		}
	}
}
