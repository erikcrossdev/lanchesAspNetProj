using LanchesProj.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace LanchesProj.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        //Carrega informações de opções de configurações 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {

        }

        //Define classes mapeadas para tabelas no banco de dados
        public DbSet<Lanche> Lanches { get; set; } //representa a tabela Lanches no banco de dados
        public DbSet<Categoria> Categorias { get; set; } //representa a tabela Categorias no banco de dados

        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; } //representa a tabela CarrinhoCompraItens no banco de dados

        public DbSet<Pedido> Pedidos { get; set; } //representa a tabela Pedidos no banco de dados
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; } //representa a tabela PedidoDetalhes no banco de dados
    }
}
