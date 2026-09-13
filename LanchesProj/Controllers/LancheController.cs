using LanchesProj.Repositories;
using LanchesProj.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesProj.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository lancheRepository; //usa a instancia

        public LancheController(ILancheRepository lancheRepository) //usar a dependecy injection
        {
            this.lancheRepository = lancheRepository;
        }

        public IActionResult List()
        {
            ViewData["Titulo"] = "Todos os Lanches";
            ViewData["Data"] = DateTime.Now;
            var lanches = lancheRepository.Lanches;

            var totalLanches = lanches.Count();
            ViewBag.Total = "Total Lanches: ";
            ViewBag.TotalLanches = totalLanches;

            return View(lanches);
        }
    }
}
