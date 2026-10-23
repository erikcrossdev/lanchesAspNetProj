using LanchesProj.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LanchesProj.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class AdminImagensController : Controller
	{
		private readonly ConfigurationImages _myConfig;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public AdminImagensController(IWebHostEnvironment hostingEnvironment, IOptions<ConfigurationImages> myConfig)
		{
			_myConfig = myConfig.Value; //options usa a classe de configuração e recuperar instancias
			_hostingEnvironment = hostingEnvironment;
		}
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> UploadFiles(List<IFormFile> files) 
		{
			if (files == null || files.Count == 0) {
				ViewData["Erro"] = "Erro: Arquivo(s) não selecionado(s)";
				return View(ViewData);
			}
			if (files.Count > 50) {
				ViewData["Erro"] = "Erro: Quantidade de arquivos excedida";
				return View(ViewData);
			}

			long size = files.Sum(file => file.Length); //tamanho da imagem

			var filePathsName = new List<string>();

			var filePath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos); //combina o caminho físico e wwwroot e a pasta das imagens

			foreach (var formFile in files)
			{
				//checa se são imagens
				if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif")
					|| formFile.FileName.Contains(".png"))
				{
					var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

					filePathsName.Add(fileNameWithPath);

					using (var stream = new FileStream(fileNameWithPath, FileMode.Create)) 
					{
						await formFile.CopyToAsync(stream);
					}
				}
			}
			ViewData["Resultado"] = $"{files.Count} arquivos enviados para o servidor com tamanho total de: {size} bytes";

			ViewBag.Arquivos = filePathsName; //conter a relação de arquivos 

			return View(ViewData);
		}

		public IActionResult GetImagens() {
			FileManagerModel model = new FileManagerModel();
			var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

			DirectoryInfo dir = new DirectoryInfo(userImagesPath);

			FileInfo[] files= dir.GetFiles();

			model.PathImagesProduto = _myConfig.NomePastaImagensProdutos;

			if (files.Length == 0)
			{
				ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
			}

			model.Files = files;
			return View(model);
		}

		public IActionResult Deletefile(string fname) {
			string imagemParaDeletar = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos + "\\", fname);

			if ((System.IO.File.Exists(imagemParaDeletar))) {
				System.IO.File.Delete(imagemParaDeletar);
				ViewData["Deletado"] = $"Arquivo(s) {imagemParaDeletar} deletado com sucesso!";
			}

			return View("index");
		}
	}
}
