using Microsoft.AspNetCore.Mvc;

namespace Teste_BRASILAPI.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class TempoController : Controller
    {
        [HttpGet]   
        public IActionResult Index()
        {
            return View();
        }
    }
}
