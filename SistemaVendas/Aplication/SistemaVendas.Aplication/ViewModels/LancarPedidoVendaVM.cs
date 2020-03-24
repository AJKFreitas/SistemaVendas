using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.ViewModels
{
   public class LancarPedidoVendaVM
    {
        public Guid Id { get; set; }
        public DateTime? DataVenda { get; set; } = DateTime.UtcNow;
        public Guid IdCliente { get; set; }
        public IEnumerable<LancarItemPedidoVendaVM> ItemPedidosVM { get; set; } = new List<LancarItemPedidoVendaVM>();
        public double ValorTotal { get; set; }
    }
}
