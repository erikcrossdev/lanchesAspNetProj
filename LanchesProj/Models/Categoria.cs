using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesProj.Models
{
    [Table("Categorias")] //Adicionando a tag Categoria para o EFCore
    public class Categoria
    {
        [Key] //reforça que CategoriaId é a chave primária da tabela
        public int CategoriaId { get; set; } //tendo o nome Id ou CategoriaId, o EFC vai setar como primary key

        [StringLength(100, ErrorMessage = "O tamanho máximo de caracteres é 100")] //define o tamanho máximo do campo
        [Required(ErrorMessage = "Informe o nome da categoria")]
        [Display(Name = "Nome")] //define o nome do campo que será exibido na tela
        public string CategoriaNome { get; set; }

        [StringLength(200, ErrorMessage = "O tamanho máximo de caracteres é 200")] //define o tamanho máximo do campo
        [Required(ErrorMessage = "Informe o nome da categoria")]
        [Display(Name = "Nome")] //define o nome do campo que se
        public string Descricao { get; set; }
        //Propriedade de navegação para ideintificar a relação entre as entidades Categoria e Lanche, ou seja, uma categoria pode ter vários lanches
        public List<Lanche> Lanches { get; set; } //propriedade de navegação para os lanches
    }
}
