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
        Task<ResultProdutoQuery> GetAll(ProdutoParams pParams);
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Guid Id);
        Task<int> Insert(ProdutoVM Produto);
        Task<int> Update(Produto Produto);
        bool ExisteProduto(long codigo);
        Task<int> Delete(Guid Id);
        Task<dynamic> CalcularEstoque(Guid idproduto);
    }
}
