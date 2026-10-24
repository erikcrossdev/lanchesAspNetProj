using LanchesProj.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchesProj.Areas.Admin.Controllers
{
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
		public IActionResult Index(int dias)
		{
			return View();
		}

		[HttpGet]
		public IActionResult VendasMensal(int dias)
		{
			return View();
		}

		[HttpGet]
		public IActionResult VendasSemanal(int dias)
		{
			return View();
		}
	}
}
