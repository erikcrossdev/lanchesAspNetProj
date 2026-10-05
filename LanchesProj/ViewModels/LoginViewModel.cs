using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesProj.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Inforeme o nome do usuário")]
		[Display(Name = "Usuário")]
		public string Username { get; set; }

		[Required(ErrorMessage ="Informe a senha")]
		[DataType(DataType.Password)] //exibe os caracteres de senha
		[Display(Name = "Senha")]
		public string Password { get; set; }

		[NotMapped] // O EF Core não vai criar essa coluna na tabela
		[DataType(DataType.Password)] //exibe os caracteres de senha
		[Display(Name = "Confirme a senha")]
		public string ConfirmPassword { get; set; }

		public string ReturnUrl { get; set; }
	}
}
