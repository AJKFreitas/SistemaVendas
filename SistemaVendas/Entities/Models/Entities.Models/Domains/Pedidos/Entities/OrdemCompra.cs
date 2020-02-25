using SistemaVendas.Core.Domains.Fornecedores.Entities;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Core.Domains.Pedidos.Entities
{
    public class OrdemCompra
    {
        public Guid Id { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Nota { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public Guid IdFornecedor { get; set; }
        public IEnumerable<ItemOrdemCompra> ItemsOrdemCompra { get; set; }
        public double ValorTotal { get; set; }

        public OrdemCompra()
        {
        }


    }
}
