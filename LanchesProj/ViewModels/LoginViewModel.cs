using System.ComponentModel.DataAnnotations;

namespace LanchesProj.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Inforeme o nome do usuário")]
		[Display(Name = "Username")]
		public string Username { get; set; }

		[Required(ErrorMessage ="Informe a senha")]
		[DataType(DataType.Password)] //exibe os caracteres de senha
		[Display(Name = "Password")]
		public string Password { get; set; }


		public string ReturnUrl { get; set; }
	}
}
