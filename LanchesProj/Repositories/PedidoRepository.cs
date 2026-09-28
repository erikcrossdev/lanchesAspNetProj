using LanchesProj.Context;
using LanchesProj.Models;
using LanchesProj.Repositories.Interfaces;

namespace LanchesProj.Repositories
{
	public class PedidoRepository : IPedidoRepository
	{
		private readonly AppDbContext _appContext;
		private readonly CarrinhoCompra _carrinhoCompra;
		public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra) 
		{
			_appContext = appDbContext;
			_carrinhoCompra = carrinhoCompra;
		}
		public void CriarPedido(Pedido pedido)
		{ 
			//Salva e persiste o pedido
			pedido.PedidoEnviado = DateTime.Now;
			_appContext.Pedidos.Add(pedido);
			_appContext.SaveChanges();

			var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItens;
			foreach(var carrinhoItem in carrinhoCompraItens)
			{
				var pedidoDetalhe = new PedidoDetalhe()
				{
					Quantidade = carrinhoItem.Quantidade,
					LancheId = carrinhoItem.Lanche.LancheId,
					PedidoId = pedido.PedidoId,
					Preco = carrinhoItem.Lanche.Preco
				};
				_appContext.PedidoDetalhes.Add(pedidoDetalhe);
			}
			_appContext.SaveChanges();
			//os itens ficam persistidos no details
		}
	}
}
