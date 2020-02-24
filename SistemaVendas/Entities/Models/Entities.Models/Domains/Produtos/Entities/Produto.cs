using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Core.Domains.Produtos.Entities
{
  public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public virtual IEnumerable<ProdutoFornecedor> ProdutoFornecedores { get; set; } = new List<ProdutoFornecedor>();
        public virtual IEnumerable<ItemPedidoVenda> ItemPedidos { get; set; } = new List<ItemPedidoVenda>();
        public virtual IEnumerable<ItemOrdemCompra> ItemOrdemCompras { get; set; } = new List<ItemOrdemCompra>();

        public Produto()
        {
        }
        public Produto(string nome, string descricao, double valor, IEnumerable<ProdutoFornecedor> produtoFornecedores, IEnumerable<ItemPedidoVenda> itemPedidos)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            ProdutoFornecedores = produtoFornecedores;
            ItemPedidos = itemPedidos;
        }


        public Produto(Guid id, string nome, string descricao, double valor, IEnumerable<ProdutoFornecedor> produtoFornecedores, IEnumerable<ItemPedidoVenda> itemPedidos)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            ProdutoFornecedores = produtoFornecedores;
            ItemPedidos = itemPedidos;
        }

        public Produto(string nome, string descricao, double valor)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
        }

        public Produto(Guid id, string nome, string descricao, double valor)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
        }

        public Produto(string nome, string descricao, double valor, IEnumerable<ProdutoFornecedor> produtoFornecedores) : this(nome, descricao, valor)
        {
            Id = Guid.NewGuid();
            ProdutoFornecedores = produtoFornecedores;
        }
    }
}
