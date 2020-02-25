using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Produtos.Interfaces
{
    public interface IProdutoRepository
    {
        Task<int> Delete(Guid Id);
        Task<PagedList<Produto>> GetAll(ProdutoParams produtoParams);
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Guid Id);
        Task<int> Insert(Produto produto);
        Task<int> Save();
        Task<int> Update(Produto produto);
        bool ExisteProduto(long codigo);
    }
}
