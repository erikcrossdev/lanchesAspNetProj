using LanchesProj.Models;
using LanchesProj.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesProj.Controllers
{
	public class CarrinhoCompraController : Controller
	{
		private readonly ILancheRepository _lancheRepository;
		private readonly CarrinhoCompra _carrinhoCompra;

		public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
		{
			_lancheRepository = lancheRepository;
			_carrinhoCompra = carrinhoCompra;
		}
		public IActionResult Index()
		{
			var itens = _carrinhoCompra.GetCarrinhoCompraItens();

			_carrinhoCompra.CarrinhoCompraItens = itens;

			var carrinhoCompraVM = new ViewModels.CarrinhoCompraViewModel
			{
				CarrinhoCompra = _carrinhoCompra,
				CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
			};


			return View(carrinhoCompraVM); //vai mostrar o carrinho de compras
		}

		//redirect to action é usado para redirecionar para outra action do mesmo controller ou de outro controller. Ele herda de action result
		public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId) {
			var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(lanche => lanche.LancheId == lancheId);
			if (lancheSelecionado != null) {
				_carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
			}
			return RedirectToAction("Index");
		}

		public IActionResult RemoverItemNoCarrinhoCompra(int lancheId)
		{
			var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(lanche => lanche.LancheId == lancheId);
			if (lancheSelecionado != null)
			{
				_carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
			}
			return RedirectToAction("Index");
		}
	}
}
