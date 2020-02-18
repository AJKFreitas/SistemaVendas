using SistemaVendas.Core.Domains.Fornecedores.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Produtos.Entities
{
  public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public virtual IEnumerable<ProdutoFornecedor> ProdutoFornecedores { get; set; } = new List<ProdutoFornecedor>();

        public Produto(Guid id, string nome, string descricao, double valor)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
        }

        public Produto(string nome, string descricao, double valor, IEnumerable<ProdutoFornecedor> produtoFornecedores)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            ProdutoFornecedores = produtoFornecedores;
        }

        public Produto()
        {
        }
    }
}
