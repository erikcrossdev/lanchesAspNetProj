using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LanchesProj.Models;

namespace LanchesProj.Controllers;

public class HomeController : Controller
{
  
    public IActionResult Index()
    {
        TempData["Mensagem"] = "Veio do HomeController!";
        return View();
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
