using LanchesMac.Areas.Admin.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private readonly RelatorioVendaService relatorioVendaService;

        public AdminRelatorioVendasController(RelatorioVendaService _relatorioVendaService)
        {
            relatorioVendaService = _relatorioVendaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> RelatorioVendasSimples(DateTime? minDate, DateTime? maxDate)
        {
            if(!minDate.HasValue) 
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);

            }

            if (!maxDate.HasValue)
            {
                maxDate =  DateTime.Now;

            }

            ViewBag.minDate = minDate.Value.ToString("yyyy-MM-dd");
            ViewBag.maxDate = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await relatorioVendaService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

    }
}
