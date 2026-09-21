using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LanchesProj.Models;
using LanchesProj.Repositories.Interfaces;
using LanchesProj.ViewModels;

namespace LanchesProj.Controllers;

public class HomeController : Controller
{
  
    private readonly ILancheRepository _lancheRepository;

    public HomeController(ILancheRepository lancheRepository)
	{
		_lancheRepository = lancheRepository;
	}

    public IActionResult Index()
    {
        TempData["Mensagem"] = "Veio do HomeController!";

        var homeViewModel = new HomeViewModel
		{
			LanchesPreferidos = _lancheRepository.LanchesPreferidos
		};

        return View(homeViewModel);
    }

    public IActionResult Demo()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
