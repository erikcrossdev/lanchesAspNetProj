namespace LanchesProj.Models
{
    public class Lanche
    {
        public int LancheId { get; set; } //tendo o nome Id ou CategoriaId, o EFC vai setar como primary key
        public string Nome { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoDetalhada { get; set; }
        public decimal Preco { get; set; }
        public string ImagemUrl { get; set; }
        public string ImagemThumbnailUrl { get; set; }
        public bool IsLanchePreferido { get; set; }
        public bool EmEstoque { get; set; }
        //Para definir o relacionamento usando a navegação e a chave estrangeira
        public int CategoriaId { get; set; } //chave estrangeira
        public virtual Categoria Categoria { get; set; } //propriedade de navegação para a categoria
    }
}
