using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        //exibição dos lanches de acordo com o filtro da url
        public IActionResult List(string categoria)
            //string categoria termo de filtro presente na url
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";

            }
            else
            {
                //verifica se a categoria é normal desconsiderando maiuscula
                if(string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lancheRepository.Lanches.
                        Where(l => l.Categoria.CategoriaNome.Equals("Normal")).
                        OrderBy(l=>l.Nome);
                }
                //verifica se a categoria é natural desconsiderando maiuscula
                else if(string.Equals("Natural", categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lancheRepository.Lanches.
                        Where(l => l.Categoria.CategoriaNome.Equals("Natural")).
                        OrderBy(l => l.Nome);
                }
                else 
                {
                    //arrumar filtro para valores não correspondentes
                    lanches = null;
                }
                categoriaAtual = categoria;
               
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
        }
    }
}
