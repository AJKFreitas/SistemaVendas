using SistemaVendas.Core.Domains.Produtos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Pedidos.Entities
{
 public class ItemOrdemCompra
    {
        public Guid Id { get; set; }
        public double Preco { get; set; }
        public long Quantidade { get; set; }
        public Produto  Produto { get; set; }
        public virtual OrdemCompra OrdemCompra { get; set; }
        public Guid IdOrdemCompra { get; set; }

        public ItemOrdemCompra()
        {

        }

    }
}
