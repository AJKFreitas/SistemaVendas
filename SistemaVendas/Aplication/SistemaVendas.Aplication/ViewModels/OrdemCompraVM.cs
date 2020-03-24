using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.ViewModels
{
   public class OrdemCompraVM
    {
        public Guid Id { get; set; }
        public Guid IdFornecedor { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Nota { get; set; }
        public virtual FornecedorVM FornecedorVM { get; set; }
        public IEnumerable<ItemOrdemCompraVM> ItemsOrdemCompraVM { get; set; } = new List<ItemOrdemCompraVM>();
        public double ValorTotal { get; set; }

    }
}
