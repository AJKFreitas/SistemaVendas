using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.ViewModels
{
    public class PedidoVendaVM
    {
        public Guid Id { get; set; }
        public DateTime Moment { get; set; }
        public virtual ClienteVM ClienteVM { get; set; }
        public Guid IdCliente { get; set; }
        public virtual IEnumerable<ItemPedidoVendaVM> ItemPedidosVM { get; set; } = new List<ItemPedidoVendaVM>();
        public double ValorTotal { get; set; }
    }
}
