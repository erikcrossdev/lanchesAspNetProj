using LanchesProj.Context;
using LanchesProj.Models;
using LanchesProj.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesProj.Repositories
{
    public class LancheRepository : ILancheRepository
    {
       private readonly AppDbContext _context; //Variavel de leitura
        public LancheRepository(AppDbContext context) {
            _context = context;
        }
        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(lanches => lanches.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.
            Where(lanche => lanche.IsLanchePreferido).
            Include(lanche => lanche.Categoria);

        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(lanche => lanche.LancheId == lancheId);
        }
    }
}
