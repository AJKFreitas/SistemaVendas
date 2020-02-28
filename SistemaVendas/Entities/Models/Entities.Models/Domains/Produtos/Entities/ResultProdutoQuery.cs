using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Produtos.Entities
{
 public   class ResultProdutoQuery
    {
        public IEnumerable <Produto> Produtos { get; set; }
        public int Total { get; set; }


    }
}
