using LanchesMac.Areas.Admin.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    public class AdminGraficosController : Controller
    {
        private readonly GraficoVendaService _graficoVendas;

        public AdminGraficosController(GraficoVendaService graficoVendas)
        {
            _graficoVendas = graficoVendas;
        }
        public JsonResult VendasLanches(int dias)
        {
            var lanchesVendasTotais = _graficoVendas.GetVendasLanche(dias);

            return Json(lanchesVendasTotais);
        }

        //vendas anuais (360 dias)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasMensal()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasSemanal()
        {
            return View();
        }
    }
}
