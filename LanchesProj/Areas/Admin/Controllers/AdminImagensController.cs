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
	}
}
