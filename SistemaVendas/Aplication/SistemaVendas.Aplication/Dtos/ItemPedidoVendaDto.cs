using SistemaVendas.Core.Domains.Produtos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.Dtos
{
  public class ItemPedidoVendaDto
    {
        public Guid Id { get; set; }
        public Guid IdProduto { get; set; }
        public Guid IdPedido { get; set; }
        public long Quantidade { get; set; }
        public double Preco { get; set; }
        public double SubTotal { get; set; }
        public virtual ProdutoDto Produto { get; set; }

        public ItemPedidoVendaDto()
        {

        }
    }
}
