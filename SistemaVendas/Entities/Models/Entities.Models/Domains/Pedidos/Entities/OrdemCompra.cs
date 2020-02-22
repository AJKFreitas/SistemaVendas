﻿using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Domains.Produtos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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


        public OrdemCompra()
        {
        }


    }
}