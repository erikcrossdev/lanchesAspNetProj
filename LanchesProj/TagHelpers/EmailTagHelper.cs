using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LanchesProj.TagHelpers
{
	public class EmailTagHelper : TagHelper
	{
		public string Endereco { get; set; }
		public string Conteudo { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "a";
			output.Attributes.SetAttribute("href", $"mailto:{Endereco}");
            output.Attributes.SetAttribute("target", "_blank"); // Adicionado para garantir o disparo do protocolo mailto
            output.Content.SetContent(Conteudo);
		}
	}
}
