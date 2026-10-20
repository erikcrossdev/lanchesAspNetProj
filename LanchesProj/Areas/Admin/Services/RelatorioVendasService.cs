using LanchesProj.Context;
using LanchesProj.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesProj.Areas.Admin.Services
{
	public class RelatorioVendasService
	{
		private readonly AppDbContext _context;

		public RelatorioVendasService(AppDbContext context)
		{ 
			_context = context;
		}

		public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) 
		{ 
			var resultado = from obj in _context.Pedidos select obj;

			if (minDate.HasValue) {
				resultado = resultado.Where(x => x.PedidoEnviado >= minDate.Value);
			}

			if (maxDate.HasValue)
			{
				resultado = resultado.Where(x => x.PedidoEnviado <= maxDate.Value);
			}

			return await resultado.
				Include(lanche => lanche.PedidoItens).
				ThenInclude(lanche => lanche.Lanche).
				OrderByDescending(x => x.PedidoEnviado).
				ToListAsync(); //to list async efetiva a consulta
		}
	}
}
