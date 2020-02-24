using SistemaVendas.Core.Domains.Produtos.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Produtos.Interfaces
{
   public interface IProdutoRepository
    {
        Task Delete(Guid EntityID);
        Task<IEnumerable<Produto>> GetAll();
        Task <Produto> GetById(Guid EntityID);
        Task<int> Insert(Produto Entity);
        Task Save();
        Task<int> Update(Produto Entity);
    }
}
