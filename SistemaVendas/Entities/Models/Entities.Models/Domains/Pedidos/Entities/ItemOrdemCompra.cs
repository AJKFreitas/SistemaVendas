using SistemaVendas.Core.Domains.Produtos.Entities;
using System;

namespace SistemaVendas.Core.Domains.Pedidos.Entities
{
    public class ItemOrdemCompra
    {
        public Guid Id { get; set; }
        public double Preco { get; set; }
        public long Quantidade { get; set; }
        public Produto Produto { get; set; }
        public Guid IdProduto { get; set; }
        public virtual OrdemCompra OrdemCompra { get; set; }
        public Guid IdOrdemCompra { get; set; }

        public ItemOrdemCompra()
        {

        }

    }
}
