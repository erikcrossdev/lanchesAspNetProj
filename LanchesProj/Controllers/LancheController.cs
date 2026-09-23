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
                //vamos mudar isso para pegar qualquer categoria
                if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase)){
                    lanches = _lancheRepository.Lanches
                        .Where(l => l.Categoria.CategoriaNome.Equals("Normal"))
                        .OrderBy(l => l.Nome);
                }
                else {
					lanches = _lancheRepository.Lanches
						.Where(l => l.Categoria.CategoriaNome.Equals("Vegetariano"))
						.OrderBy(l => l.Nome);
				}
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
