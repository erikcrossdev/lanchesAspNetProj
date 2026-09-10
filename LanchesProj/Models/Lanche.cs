using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesProj.Models
{
    [Table("Lanches")] //mapeia para o nome de tabela Lanches, caso não seja definido, o EFC vai criar uma tabela com o nome da classe
    public class Lanche
    {
        [Key] //reforça que será a key da tabela
        public int LancheId { get; set; } //tendo o nome Id ou CategoriaId, o EFC vai setar como primary key

        [Required(ErrorMessage = "Informe o nome do lanche")] //não pode ser nulo
        [Display(Name = "Nome do Lanche")] //define o nome que será exibido na tela
        [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e máximo {2}")] //define o tamanho máximo e mínimo do campo
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a descrição curta do lanche")]
        [Display(Name = "Descrição curta do Lanche")]
        [MinLength(20, ErrorMessage ="Descrição deve ter no mínimo {1} caracteres")] //tamanho minimo
        [MaxLength(200, ErrorMessage = "Descrição deve ter no máximo {1} caracteres")] //tamanho máximo
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "Informe a descrição detalhada do lanche")]
        [Display(Name = "Descrição Detalhada do Lanche")]
        [MinLength(40, ErrorMessage = "Descrição Detalhada deve ter no mínimo {1} caracteres")] //tamanho minimo
        [MaxLength(400, ErrorMessage = "Descrição Detalhada deve ter no máximo {1} caracteres")] //tamanho máximo
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name = "Preço do Lanche")]
        [Column(TypeName = "decimal(10,2)")] //define o tipo de dado no banco de dados e a precisão
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre {1} e {2}")] //define o intervalo de valores
        public decimal Preco { get; set; }
        [Display(Name = "Imagem do Lanche")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caractere")] //define o tamanho máximo e mínimo do campo
        public string ImagemUrl { get; set; }

        [Display(Name = "Thumbnail do Lanche")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caractere")] //define o tamanho máximo e mínimo do campo
        public string ImagemThumbnailUrl { get; set; }
        
        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }
       
        //propriedades de navegação para definir o relacionamento entre as entidades Lanche e Categoria, ou seja, um lanche pertence a uma categoria
        public int CategoriaId { get; set; }  //Para definir o relacionamento usando a navegação e a chave estrangeira
        public virtual Categoria Categoria { get; set; } //propriedade de navegação para a categoria
    }
}
