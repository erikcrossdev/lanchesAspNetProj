using LanchesProj.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesProj.Context
{
    public class AppDbContext : DbContext
    {
        //Carrega informações de opções de configurações 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {

        }

        //Define classes mapeadas para tabelas no banco de dados
        public DbSet<Lanche> Lanches { get; set; } //representa a tabela Lanches no banco de dados
        public DbSet<Categoria> Categorias { get; set; } //representa a tabela Categorias no banco de dados
    }
}
