using SistemaVendas.Infra.Data;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.Core.Shared.Entities;
using SistemaVendas.Core.Shared.Interfaces;
using SistemaVendas.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Infra.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
    {
        public abstract void Delete(Guid EntityID);
        public abstract IEnumerable<TEntity> GetAll();
        public abstract TEntity GetById(Guid EntityID);
        public abstract void Insert(TEntity Entity);
        public abstract void Save();
        public abstract void Update(TEntity Entity);
        public abstract void Dispose();
    }
}
