using LanchesProj.Context;

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

	}
}
