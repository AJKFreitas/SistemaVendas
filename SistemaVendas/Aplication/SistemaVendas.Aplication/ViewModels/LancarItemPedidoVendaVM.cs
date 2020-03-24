using System;

namespace SistemaVendas.Aplication.ViewModels
{
    public class LancarItemPedidoVendaVM
    {
        public Guid? Id { get; set; }
        public Guid IdProduto { get; set; }
        public Guid? IdPedido { get; set; }
        public long Quantidade { get; set; }
        public double Preco { get; set; }
        public double SubTotal { get; set; }
    }
}