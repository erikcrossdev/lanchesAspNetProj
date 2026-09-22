using LanchesProj.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanchesProj.Components
{
	public class CarrinhoCompraResumo : ViewComponent
	{
		private readonly CarrinhoCompra _carrinhoCompra;

		public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
		{
			_carrinhoCompra = carrinhoCompra;
		}

		public IViewComponentResult Invoke()
		{
			var itens = _carrinhoCompra.GetCarrinhoCompraItens();

			//Mock cart
			/*
			var itens = new List<CarrinhoCompraItem> {
                new CarrinhoCompraItem { Lanche = new Lanche { Nome = "X-Burguer", Preco = 9.99M }, Quantidade = 2 },
                new CarrinhoCompraItem { Lanche = new Lanche { Nome = "X-Salada", Preco = 12.99M }, Quantidade = 1 },
                new CarrinhoCompraItem { Lanche = new Lanche { Nome = "X-Bacon", Preco = 14.99M }, Quantidade = 3 }
            };*/

			_carrinhoCompra.CarrinhoCompraItens = itens;

			var carrinhoCompraVM = new ViewModels.CarrinhoCompraViewModel
			{
				CarrinhoCompra = _carrinhoCompra,
				CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
			};


			return View(carrinhoCompraVM); //vai mostrar o carrinho de compras	

		}
	}
}
