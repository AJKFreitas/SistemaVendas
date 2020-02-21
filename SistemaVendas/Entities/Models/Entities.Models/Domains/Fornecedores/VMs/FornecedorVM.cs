using SistemaVendas.Core.Domains.Produtos.VMs;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Core.Domains.Fornecedores.VMs

{
    public class FornecedorVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public long CNPJ { get; set; }
        public IEnumerable<ProdutoVM> ProdutoFornecedores { get; set; }
    }
}
