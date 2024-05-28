using Microsoft.AspNetCore.Mvc;
using Teste_BRASILAPI.Services;

namespace Teste_BRASILAPI.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class TempoController : Controller
    {
        private readonly TempoService _tempoService;

        public TempoController(TempoService tempoService)
        {
            _tempoService = tempoService;
        }

        [HttpGet]   
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{action}")]
        public async IActionResult BuscarCidade(string cidade)
        {
            var response = await _tempoService.BuscarCidade(cidade);

            return View(response);
        }
    }
}
