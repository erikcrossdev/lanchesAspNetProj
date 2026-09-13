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
            var lanches = lancheRepository.Lanches;
            return View(lanches);
        }
    }
}
