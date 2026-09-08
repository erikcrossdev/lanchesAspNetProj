namespace LanchesProj.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; } //tendo o nome Id ou CategoriaId, o EFC vai setar como primary key
        public string CategoriaNome { get; set; }
        public string Descricao { get; set; }
        //Propriedade de navegação para ideintificar a relação entre as entidades Categoria e Lanche, ou seja, uma categoria pode ter vários lanches
        public List<Lanche> Lanches { get; set; } //propriedade de navegação para os lanches
    }
}
