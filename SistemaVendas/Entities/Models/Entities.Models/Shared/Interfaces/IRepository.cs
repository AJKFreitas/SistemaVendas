using System;
using System.Collections.Generic;

namespace SistemaVendas.Core.Shared.Interfaces
{
  public  interface IRepository<T> : IDisposable
    {
        public  void Delete(Guid EntityID);
        public IEnumerable<T> GetAll();
        public T GetById(Guid EntityID);
        public  void Insert(T Entity);
        public  void Save();
        public  void Update(T Entity);
    }
}
