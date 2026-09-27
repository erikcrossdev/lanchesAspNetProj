using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesProj.Models
{
	public class Pedido
	{
		public int PedidoId { get; set; }

		[Required(ErrorMessage = "Informe o nome do cliente")]
		[StringLength(50)]
		public string Nome { get; set; }
		[Required(ErrorMessage = "Informe o sobrenome do cliente")]
		[StringLength(50)]
		public string Sobrenome { get; set; }

		[Required(ErrorMessage = "Informe o endereço")]
		[StringLength(100)]
		[Display(Name = "Endereço")]
		public string Endereco1 { get; set; }

		[Required(ErrorMessage = "Informe o endereço")]
		[StringLength(100)]
		[Display(Name = "Complemento")]
		public string Endereco2 { get; set; }

		[Required(ErrorMessage = "Informe o CEP")]
		[StringLength(10, MinimumLength = 8)]
		[Display(Name = "CEP")]
		public string Cep { get; set; }

		[StringLength(10, MinimumLength = 8)]
		public string Estado { get; set; }

		[StringLength(50, MinimumLength = 8)]
		public string Cidade { get; set; }

		[Required(ErrorMessage = "Informe o telefone")]
		[StringLength(25, MinimumLength = 8)]
		[DataType(DataType.PhoneNumber)]
		public string Telefone { get; set; }

		[Required(ErrorMessage = "Informe o email")]
		[StringLength(50)]
		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "E-mail em formato inválido")]
		public string Email { get; set; }

		[ScaffoldColumn(false)] //Usado para não mostrar na view
		[Column(TypeName = "decimal(18,2)")]
		[Display(Name = "Total do pedido")]
		public decimal PedidoTotal { get; set; }

		[ScaffoldColumn(false)]
		[Display(Name = "Itens do pedido")]
		public int TotalItensPedido { get; set; }

		[Display(Name = "Data do pedido")]
		[DataType(DataType.Text)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
		public DateTime PedidoEnviado { get; set; }

		[Display(Name = "Data do Envio do pedido")]
		[DataType(DataType.Text)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
		public DateTime? PedidoEntregueEm { get; set; }

		public List<PedidoDetalhe> PedidoDetalhes { get; set; }


	}
}
