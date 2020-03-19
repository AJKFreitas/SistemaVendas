using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.ViewModels
{
    public class PedidoVendaVM
    {
        public Guid  Id { get; set; }
        public DateTime? DataVenda { get; set; }  = DateTime.Now;
        public Guid IdCliente { get; set; }
        public IEnumerable<ItemPedidoVendaVM> ItemPedidosVM { get; set; } = new List<ItemPedidoVendaVM>();
        public double ValorTotal { get; set; }
    }
}
