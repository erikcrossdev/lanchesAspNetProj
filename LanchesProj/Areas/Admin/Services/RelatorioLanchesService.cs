using LanchesProj.Context;
using LanchesProj.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesProj.Areas.Admin.Services
{
	public class RelatorioLanchesService
	{
		private readonly AppDbContext _context;

		public RelatorioLanchesService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Lanche>> GetLancheReport() {
			var lanches = await _context.Lanches.ToListAsync();

			if (lanches is null) {
				return default(IEnumerable<Lanche>); //lista vazia, default
			}

			return lanches;
		}


		public async Task<IEnumerable<Categoria>> GetCategoriaReport()
		{
			var categoria = await _context.Categorias.ToListAsync();

			if (categoria is null)
			{
				return default(IEnumerable<Categoria>); //lista vazia, default
			}

			return categoria;
		}
	}
}
