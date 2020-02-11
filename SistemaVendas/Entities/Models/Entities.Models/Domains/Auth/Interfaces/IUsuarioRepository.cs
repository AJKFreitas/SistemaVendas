using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Infra.Data.Interfaces
{
   public interface IUsuarioRepository : IRepository<Usuario>
    {
        IEnumerable<Usuario> GetAll();
        Usuario GetById(Guid EntityID);
        void Insert(Usuario Entity);
        void Update(Usuario Entity);
        void Delete(Guid EntityID);
        void Save();
    }
}
