using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesProj.Migrations
{
    /// <inheritdoc />
    public partial class PopularLanches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Lanches (CategoriaId, DescricaoCurta, DescricaoDetalhada, EmEstoque, ImagemThumbnailUrl, ImagemUrl, IsLanchePreferido, Nome, Preco) VALUES (1, 'Pão, ovo e hamburguer', 'Pão, ovo e hamburguer, contém gluten', 1, 'http://www.google.com',  'http://www.google.com', 0, 'Hamburguer', 12.50 )");
            migrationBuilder.Sql("INSERT INTO Lanches (CategoriaId, DescricaoCurta, DescricaoDetalhada, EmEstoque, ImagemThumbnailUrl, ImagemUrl, IsLanchePreferido, Nome, Preco) VALUES (2, 'Pão integral, ovo e hamburguer', 'Pão integral, ovo e hamburguer, contém gluten', 1, 'http://www.google.com',  'http://www.google.com', 1, 'Hamburguer (integral)', 12.50 )");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Lanches"); //remove tudo da tabela Lanches.
        }
    }
}
