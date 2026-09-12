using LanchesProj.Models;
using LanchesProj.Repositories.Interfaces;
using LanchesProj.Context;

namespace LanchesProj.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public IEnumerable<Categoria> Categorias => _context.Categorias;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
