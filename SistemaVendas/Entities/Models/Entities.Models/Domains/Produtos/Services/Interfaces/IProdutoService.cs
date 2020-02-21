using SistemaVendas.Core.Domains.Produtos.VMs;
using SistemaVendas.Core.Domains.Produtos.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Produtos.Services.Interfaces
{
   public interface IProdutoService
    {
        Task Delete(Guid Id);
        Task<IAsyncEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Guid Id);
        Task<int> Insert(ProdutoVM Produto);
        Task<int> Update(Produto Produto);
    }
}
