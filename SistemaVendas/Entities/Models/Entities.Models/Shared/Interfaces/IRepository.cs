using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Shared.Interfaces
{
  public  interface IRepository<TEntity> : IDisposable
    {
        public  void Delete(Guid EntityID);
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<TEntity> GetById(Guid EntityID);
        public  void Insert(TEntity Entity);
        public  void Save();
        public  void Update(TEntity Entity);
        public  void Dispose();
    }
}
