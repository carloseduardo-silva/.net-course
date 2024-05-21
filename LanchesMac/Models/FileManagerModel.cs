namespace LanchesMac.Models
{
    public class FileManagerModel
    {

        //leitura e acesso dos arquivos
        public FileInfo[] Files { get; set; }

        public IFormFile IFormFile { get; set; }
        public List<IFormFile> IFormFiles { get; set; }

        public string PathImagesProduto {  get; set; }  
    }
}
