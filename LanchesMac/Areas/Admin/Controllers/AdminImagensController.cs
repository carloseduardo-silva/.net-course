using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {

        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public AdminImagensController(IOptions<ConfigurationImagens> myConfiguration, 
            IWebHostEnvironment hostingEnviroment)
        {
            _myConfig = myConfiguration.Value;
            _hostingEnviroment = hostingEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            //verificação se é imagem
           if(files == null || files.Count == 0)
            {
                ViewBag.erro = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewBag);
            }

            if (files.Count > 10)
            {
                ViewBag.erro = "Error: Quantidade de arquivos excedeu o limite";
                return View(ViewBag);
            }

            //soma o tamanho do arquivo + monta url
            long size = files.Sum(f => f.Length);

            var filePathsName = new List<string>();

            var filePath = Path.Combine(_hostingEnviroment.WebRootPath, _myConfig.NomePastaImagensProdutos);

            //montagem da url
            foreach(var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".png") ||
                    formFile.FileName.Contains(".gif"))
                {
                    var fileNameWithPath = string.Concat(filePath,"\\", formFile.FileName);

                    filePathsName.Add(fileNameWithPath);    

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);

                    }
                }
            }
            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, " +  $" com tamanho toal de: {size} bytes";
            ViewBag.Arquivos = filePathsName;
            return View(ViewBag);   
        }

        public IActionResult GetImages()
        {
            FileManagerModel model = new FileManagerModel();
            //caminho da imagem
            var userImagesPath = Path.Combine(_hostingEnviroment.WebRootPath, _myConfig.NomePastaImagensProdutos);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);

            FileInfo[] files = dir.GetFiles();

            model.PathImagesProduto = _myConfig.NomePastaImagensProdutos;

            if(files.Length == 0)
            {
                ViewBag.erro = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
            }
            model.Files = files;

             return View(model);
        }

        public IActionResult DeleteFile(string fname)
        {
            
            //caminho da imagem
            string _imagemDeletada = Path.Combine(_hostingEnviroment.WebRootPath,
                _myConfig.NomePastaImagensProdutos + "\\", fname);

            if(System.IO.File.Exists(_imagemDeletada))
            {
                System.IO.File.Delete(_imagemDeletada);

                ViewData["Deletado"] = $"Arquivo(s) {_imagemDeletada} deletado com sucesso";
            }

            return View("index");
        }

    }
}
