using LanchesProj.Models;
using System.Collections;

namespace LanchesProj.ViewModels
{
	public class PedidoLancheViewModel
	{
		public Pedido Pedido { get; set; }

		public IEnumerable<PedidoDetalhe> PedidoDetalhe { get; set; }
	}
}
