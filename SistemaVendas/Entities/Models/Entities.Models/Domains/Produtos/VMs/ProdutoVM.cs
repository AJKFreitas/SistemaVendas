using SistemaVendas.Core.Domains.Fornecedores.VMs;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Core.Domains.Produtos.VMs


{
    public class ProdutoVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public IEnumerable<FornecedorVM> ProdutoFornecedores { get; set; }
    }
}
