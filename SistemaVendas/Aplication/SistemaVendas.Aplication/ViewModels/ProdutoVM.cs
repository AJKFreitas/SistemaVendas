using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.ViewModels
{
    public class ProdutoVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public IEnumerable<FornecedorVM> Fornecedores { get; set; } = new List<FornecedorVM>();
    }
}
