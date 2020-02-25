using SistemaVendas.Core.Domains.Produtos.Entities;
using System;

namespace SistemaVendas.Core.Domains.Fornecedores.Entities
{
    public class ProdutoFornecedor
    {
        public Guid Id { get; set; }
        public Guid IdProduto { get; set; }
        public Guid IdFornecedor { get; set; }
        public Produto Produto { get; set; }
        public Fornecedor Fornecedor { get; set; } = new Fornecedor();

        public ProdutoFornecedor()
        {
            Id = Guid.NewGuid();
        }

        public ProdutoFornecedor(Guid idFornecedor, Guid idProduto)
        {
            Id = Guid.NewGuid();
            IdProduto = idProduto;
            IdFornecedor = idFornecedor;
        }
    }
}
