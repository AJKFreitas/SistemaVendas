using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Produtos.Interfaces
{
    public interface IProdutoRepository
    {
        Task<PagedList<Produto>> BuscarPorFiltroComPaginacao(ProdutoParams produtoParams);
        Task<IEnumerable<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(Guid Id);
        Task<int> Inserir(Produto produto);
        Task<int> Editar(Produto produto);
        Task<int> Excluir(Guid Id);
        Task<int> SalvarCommit();
        bool ExisteProduto(long codigo);
        Task<dynamic> CalcularEstoque(Guid idproduto);
    }
}
