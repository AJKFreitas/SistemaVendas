using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Produtos
{
   public interface IProdutoService
    {
        Task<PagedList<Produto>> BuscarPorFiltroComPaginacao(ProdutoParams pParams);
        Task<IEnumerable<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(Guid Id);
        Task<int> Inserir(ProdutoVM Produto);
        Task<int> Editar(Produto Produto);
        Task<int> Excluir(Guid Id);
        bool ExisteProduto(long codigo);
        Task<dynamic> CalcularEstoque(Guid idproduto);
    }
}
