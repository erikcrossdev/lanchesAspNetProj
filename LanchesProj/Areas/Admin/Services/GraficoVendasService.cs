using LanchesProj.Context;
using LanchesProj.Models;

namespace LanchesProj.Areas.Admin.Services
{
	public class GraficoVendasService
	{
		private readonly AppDbContext _context;

		public GraficoVendasService(AppDbContext context)
		{
			_context = context;
		}

		public List<LancheGrafico> GetVendasLanches(int dias = 360)
		{
			var data = DateTime.Now.AddDays(-dias);

			var lanches = from pedidoDetalhe in _context.PedidoDetalhes //pega pedidos
						  join lanche in _context.Lanches on pedidoDetalhe.LancheId equals lanche.LancheId //junta dados de lanche e pedido detalhe quando o id bate
						  where pedidoDetalhe.Pedido.PedidoEnviado >= data  //seleciona com where se a data for maior ou igual que data
						  group pedidoDetalhe by new { pedidoDetalhe.LancheId, lanche.Nome} //agrupa dados de tabelas diferentes, quantidade e id de pedido e name de lanches
						  into g  //funciona como uma 'segunda consulta'
						  select new {  //gera um tipo anonimo, precisamos converter em uma list de LancheGrafico
							  LancheNome = g.Key.Nome,
							  LanchesQuantidade = g.Sum(quant => quant.Quantidade),
							  LanchesValorTotal = g.Sum(t => t.Preco * t.Quantidade)
						  };

			var list = new List<LancheGrafico>();

			foreach (var item in lanches) {
				var lanche = new LancheGrafico();
				lanche.LancheNome = item.LancheNome;
				lanche.LanchesQuantidade = item.LanchesQuantidade;
				lanche.LanchesValorTotal = item.LanchesValorTotal;		
				list.Add(lanche);
			}
			return list;
		}
	}
}
