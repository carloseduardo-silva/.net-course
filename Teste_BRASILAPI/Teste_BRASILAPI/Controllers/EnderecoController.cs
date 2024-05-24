using Microsoft.AspNetCore.Mvc;
using Teste_BRASILAPI.Interfaces;
using Teste_BRASILAPI.Services;

namespace Teste_BRASILAPI.Controllers
{
    [ApiController]
    [Route("api/v1/controller")]
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

        [HttpGet("busca/{cep}")]
       
        
        public async Task<IActionResult> EnderecoInfo([FromRoute] string cep)
        {
            var response = await _enderecoService.BuscarEndereco(cep);

           return View(response);
        }
    }
}
