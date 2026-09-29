using LanchesProj.Models;
using LanchesProj.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesProj.Controllers
{
	public class PedidoController : Controller
	{
		private readonly IPedidoRepository _pedidoRepository;
		private readonly CarrinhoCompra _carrinhoCompra;

		public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
		{
			_pedidoRepository = pedidoRepository;
			_carrinhoCompra = carrinhoCompra;
		}

		[HttpGet]
		public IActionResult Checkout()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Checkout(Pedido pedido) {
			int totalItensPedido = 0;
			decimal precoTotalPedido = 0.0m;

			//obter os itens do carrinho 
			List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhoCompraItens();
			_carrinhoCompra.CarrinhoCompraItens = itens;

			//verifica se existe itens
			if (_carrinhoCompra.CarrinhoCompraItens.Count == 0) 
			{
				ModelState.AddModelError("", "Seu carrinho está vazio, que tal incluir um lanche?");
			}

			//Calcula o total de itens e do pedido
			foreach(var item in itens) 
			{
				totalItensPedido += item.Quantidade;
				precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
			}

			//atribuir os valores obtidos ao pedido
			pedido.TotalItensPedido = totalItensPedido;
			pedido.PedidoTotal = precoTotalPedido;

			//Validar dados do pedido
			if (ModelState.IsValid) 
			{
				//criar pedido e detalhes dele 
				_pedidoRepository.CriarPedido(pedido);

				//define mensagens ao cliente 
				ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
				ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

				//limpa o carrinho do cliente
				_carrinhoCompra.LimparCarrinho();

				//Exibe a view com dados do cliente e do pedido
				return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
			}
			return View(pedido);
		}

	}
}
