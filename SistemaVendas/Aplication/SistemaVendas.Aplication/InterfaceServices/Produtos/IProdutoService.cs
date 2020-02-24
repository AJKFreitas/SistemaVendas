using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Produtos.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Produtos
{
   public interface IProdutoService
    {
        Task Delete(Guid Id);
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Guid Id);
        Task<int> Insert(ProdutoVM Produto);
        Task<int> Update(Produto Produto);
    }
}
