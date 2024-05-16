using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _categoriarepository;

        public CategoriaMenu(ICategoriaRepository categoriarepository)
        {
            _categoriarepository = categoriarepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoriarepository.Categorias.OrderBy(p => p.CategoriaNome);

            return View(categorias);

        }
    }
}
