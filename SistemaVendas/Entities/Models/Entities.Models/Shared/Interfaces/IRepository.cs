using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Shared.Interfaces
{
  public  interface IRepository<TEntity> : IDisposable
    {
        public  void Delete(Guid EntityID);
        public  IEnumerable<TEntity> GetAll();
        public  TEntity GetById(Guid EntityID);
        public  void Insert(TEntity Entity);
        public  void Save();
        public  void Update(TEntity Entity);
        public  void Dispose();
    }
}
