using System;
using System.Collections.Generic;

namespace SistemaVendas.Aplication.ViewModels
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
