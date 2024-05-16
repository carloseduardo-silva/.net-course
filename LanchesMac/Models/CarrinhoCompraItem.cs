using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("CarrinhoCompraItems")]
    public class CarrinhoCompraItem
    {
        public int CarrinhoCompraItemId { get; set; }
        //id do item
        public Lanche Lanche { get; set; }

        public int Quantidade { get; set; }

        [StringLength(200)]
        public string CarrinhoCompraId { get; set; }
        //id da compra no carrinho
    }
}
