using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class AdminController : Controller
    {
        public string Index()
        {
            return "view admin, Testando view admin";
        }
        public string Demo()
        {
            return "view admin, Testando view demo";
        }

    }
}
