using SistemaVendas.Core.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Infra.Data.Repository
{
    public abstract class Repository<T> : IDisposable
    {
        public abstract void Delete(Guid EntityID);
        public abstract IEnumerable<T> GetAll();
        public abstract T GetById(Guid EntityID);
        public abstract void Insert(T Entity);
        public abstract void Save();
        public abstract void Update(T Entity);
        public abstract void Dispose();
    }
}
