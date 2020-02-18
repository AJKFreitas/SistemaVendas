using SistemaVendas.Core.Domains.Fornecedores.Entities;
using System;

namespace SistemaVendas.Core.Domains.Produtos.Dtos

{
  public  class ProdutoVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
