using LanchesProj.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesProj.Models
{
	public class CarrinhoCompra
	{
		private readonly AppDbContext _context;

		public CarrinhoCompra(AppDbContext context)
		{
			_context = context;
		}

		public string CarrinhoCompraId { get; set; }

		public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
		public static CarrinhoCompra GetCarrinho(IServiceProvider services) { 
			//Definie uma session
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

			//obtem um serviço do tipo do nosso acesso
			var context = services.GetService<AppDbContext>();

			//obtem ou gera Id do carrinho
			string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

			//atribui o id do carrinho na sessão
			session.SetString("CarrinhoId", carrinhoId);

			return new CarrinhoCompra(context)
			{
				CarrinhoCompraId = carrinhoId
			};
		}

		public void AdicionarAoCarrinho(Lanche lanche) {
		
			var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
				carrinho => carrinho.Lanche.LancheId == lanche.LancheId && carrinho.CarrinhoCompraId == CarrinhoCompraId);

			if (carrinhoCompraItem == null)
			{
				carrinhoCompraItem = new CarrinhoCompraItem
				{
					CarrinhoCompraId = CarrinhoCompraId,
					Lanche = lanche,
					Quantidade = 1
				};
				_context.CarrinhoCompraItens.Add(carrinhoCompraItem);
			}
			else {
				carrinhoCompraItem.Quantidade++; //Já existe, então adiciona mais um item do mesmo lanche
			}

			_context.SaveChanges();

		}

		public int RemoverDoCarrinho(Lanche lanche) {
			var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
				carrinho => carrinho.Lanche.LancheId == lanche.LancheId && carrinho.CarrinhoCompraId == CarrinhoCompraId);

			var quantidadeLocal = 0;

			if (carrinhoCompraItem != null) {
				if(carrinhoCompraItem.Quantidade > 1) {
					carrinhoCompraItem.Quantidade--;
					quantidadeLocal = carrinhoCompraItem.Quantidade;
				}
				else {
					_context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
				}
			
			}
			_context.SaveChanges();
			return quantidadeLocal;
		}

		public List<CarrinhoCompraItem> GetCarrinhoCompraItens() {
			return CarrinhoCompraItens ?? (CarrinhoCompraItens = _context.CarrinhoCompraItens
				.Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId)
				.Include(compraItem=> compraItem.Lanche)
				.ToList());
		}

		public void LimparCarrinho() {
			var carrinhoItens = _context.CarrinhoCompraItens
				.Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

			_context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
			_context.SaveChanges();
		}

		public decimal GetCarrinhoCompraTotal() {
			var total = _context.CarrinhoCompraItens
				.Where(carrinho=> carrinho.CarrinhoCompraId == CarrinhoCompraId)
				.Select(compra =>compra.Lanche.Preco * compra.Quantidade).Sum();
			return total;
		}

	}
}
