using FastReport.Data;
using FastReport.Web;
using LanchesProj.Areas.Admin.FastReportUtils;
using LanchesProj.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using FastReport.Export.PdfSimple;

namespace LanchesProj.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminLanchesReportController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly RelatorioLanchesService _relatorioLanchesService;

		public AdminLanchesReportController(IWebHostEnvironment webHostEnvironment, RelatorioLanchesService relatorioLanchesService)
		{
			_webHostEnvironment = webHostEnvironment;
			_relatorioLanchesService = relatorioLanchesService;
		}

		public async Task<ActionResult> LanchesCategoriasReport() {
			var webReport = new WebReport();
			var mssqlDataConnection = new MsSqlDataConnection();

			webReport.Report.Dictionary.AddChild(mssqlDataConnection); //registra as conexões do sql

			//Pega o caminho físico do server com o path e arquivo
			webReport.Report.Load(Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/reports", "LanchesCategoria.frx"));

			var lanches = HelperFastReport.GetTable(await _relatorioLanchesService.GetLancheReport(), "LanchesReport");
			var categorias = HelperFastReport.GetTable(await _relatorioLanchesService.GetCategoriaReport(), "CategoriasReport");

			webReport.Report.RegisterData(lanches, "LanchesReport");
			webReport.Report.RegisterData(categorias, "CategoriasReport");

			return View("LanchesCategoriasReport", webReport);
		}

		private async Task<Stream> GeneratePDF() {
			var webReport = new WebReport();
			var mssqlDataConnection = new MsSqlDataConnection();

			webReport.Report.Dictionary.AddChild(mssqlDataConnection); //registra as conexões do sql

			//Pega o caminho físico do server com o path e arquivo
			webReport.Report.Load(Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/reports", "LanchesCategoria.frx"));

			var lanches = HelperFastReport.GetTable(await _relatorioLanchesService.GetLancheReport(), "LanchesReport");
			var categorias = HelperFastReport.GetTable(await _relatorioLanchesService.GetCategoriaReport(), "CategoriasReport");

			webReport.Report.RegisterData(lanches, "LanchesReport");
			webReport.Report.RegisterData(categorias, "CategoriasReport");

			//Gera o PDF
			webReport.Report.Prepare();

			Stream stream = new MemoryStream();

			webReport.Report.Export(new PDFSimpleExport(), stream);
			stream.Position = 0;

			return stream;
		}

		public async Task<ActionResult> LanchesCategoriaPDF()
		{
			Stream stream = await GeneratePDF();

			return File(stream, "application/zip", "LancheCategoria.pdf"); //download do arquivo
		}



		public async Task<ActionResult> LanchesCategoriaAbrePDF()
		{
			Stream stream = await GeneratePDF();

			return new FileStreamResult(stream, "application/pdf"); //abre no navegador
		}

	}
}
