using SistemaVendas.Core.Domains.Produtos.Dtos;
using SistemaVendas.Core.Domains.Produtos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Fornecedores.Dtos
{
    public class FornecedorVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public long CNPJ { get; set; }
        public IEnumerable<ProdutoVM> ProdutosFornecidos { get; set; }
        public FornecedorVM()
        {

        }

        public FornecedorVM(Guid id, string nome, string telefone, long cNPJ, IEnumerable<ProdutoVM> produtosFornecidos)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CNPJ = cNPJ;
            ProdutosFornecidos = produtosFornecidos;
        }
    }
}
