using Microsoft.AspNetCore.Mvc;
using Teste_BRASILAPI.Interfaces;
using Teste_BRASILAPI.Services;

namespace Teste_BRASILAPI.Controllers
{
    [ApiController]
    [Route("endereco/{cep}")]
    public class EnderecoController : Controller
    {

        private readonly EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> EnderecoInfo( string cep)
        {
            var response = await _enderecoService.BuscarEndereco(cep);

           return View(response);
        }

  
    }
}
