using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.ViewModels
{
   public class LancarOrdemCompraVM
    {
        public Guid Id { get; set; }
        public Guid IdFornecedor { get; set; }
        public DateTime? DataEntrada { get; set; } = DateTime.UtcNow;
        public IEnumerable<LancarItemOrdemCompraVM> ItemsOrdemCompraVM { get; set; } = new List<LancarItemOrdemCompraVM>();
        public double ValorTotal { get; set; }
    }
}
