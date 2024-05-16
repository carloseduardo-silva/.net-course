using LanchesMac.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            //instancia do banco de dados
            _context = context;
        }

        public string CarrinhoCompraId {  get; set; }

        public List <CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //DEFINE UMA SESSAO
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //OBTEM UM SERVICO DO TIPO DO NOSSO CONTEXTO
            var context = services.GetService<AppDbContext>();

            //gera ou obtem id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //retorna o carrinho com contexto + id 
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche) 
        {
            //verifica se o lanche ja esta no carrinho pelo seu ID
            var carrinhoCompraItem =  _context.CarrinhoCompraItems.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId && 
                s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                //se nao estiver cria item e seus atributos
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1

                };
             }
            else
            {
                //caso estiver apenas add mais uma unidade
                carrinhoCompraItem.Quantidade = 1;

            }
            //salvar no db
            _context.SaveChanges();
        }

        public void RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItems.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraId);

            

            if(carrinhoCompraItem != null)
            {
                if(carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    
                }
            }
            else
            {
                _context.CarrinhoCompraItems.Remove(carrinhoCompraItem);
            }
            _context.SaveChanges();
           
        }

        public List<CarrinhoCompraItem>GetCarrinhoCompraItems() 
        {
            return CarrinhoCompraItems ?? (CarrinhoCompraItems = _context.CarrinhoCompraItems.
                Where(c => c.CarrinhoCompraId == CarrinhoCompraId).
                Include(s => s.Lanche).ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItems.Where(c => c.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItems.RemoveRange(carrinhoItens);
            _context.SaveChanges();

        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItems.Where(c => c.CarrinhoCompraId == CarrinhoCompraId).
                Select(c => c.Lanche.Preco * c.Quantidade).Sum();

            return total;

        }
    }

   
}
