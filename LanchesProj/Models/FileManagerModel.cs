namespace LanchesProj.Models
{
	public class FileManagerModel
	{
		public FileInfo[] Files { get; set; }
		public IFormFile IFormFile { get; set; }	//permite mandar arquivos 
		public List<IFormFile> IFormFiles { get; set; } //lista de arquivos
		public string PathImagesProduto { get; set; }
	}
}
