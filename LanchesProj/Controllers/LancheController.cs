using LanchesProj.Models;
using LanchesProj.Repositories;
using LanchesProj.Repositories.Interfaces;
using LanchesProj.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesProj.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository; //usa a instancia

        public LancheController(ILancheRepository lancheRepository) //usar a dependecy injection
        {
            this._lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;
            
            if(string.IsNullOrEmpty(categoria))
			{
				lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
				categoriaAtual = "Todos os Lanches";
			}
			else
			{
               lanches = _lancheRepository.Lanches
                    .Where(l => l.Categoria.CategoriaNome==categoria)
                    .OrderBy(l => l.Nome);
                categoriaAtual = categoria; 
			}
			
            var lanchesListViewModel = new LancheListViewModel
			{
				Lanches = lanches,
				CategoriaAtual = categoriaAtual
			};

			return View(lanchesListViewModel);
        }
    }
}
