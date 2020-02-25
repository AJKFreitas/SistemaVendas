using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Shared.Interfaces
{
    public interface IRepository<T>
    {
        Task<int> Delete(Guid Id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task<int> Insert(T T);
        Task<int> Save();
        Task<int> Update(T T);
    }
}
