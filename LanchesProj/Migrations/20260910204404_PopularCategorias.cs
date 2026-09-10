using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesProj.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome, Descricao) "+
                "VALUES ('Normal', 'Lanche feito com carne')");

            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome, Descricao) " +
           "VALUES ('Vegetariano', 'Lanche feito sem produtos de origem animal')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias"); //remove tudo da tabela Categorias
        }
    }
}
