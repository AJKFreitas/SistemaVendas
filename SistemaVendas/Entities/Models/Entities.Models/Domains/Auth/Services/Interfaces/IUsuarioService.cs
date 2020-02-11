using SistemaVendas.Core.Domains.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Auth.Services.Interfaces
{
   public interface IUsuarioService
    {
        IEnumerable<Usuario> GetAll();
        Usuario GetById(Guid EntityID);
        void Insert(Usuario Entity);
        void Update(Usuario Entity);
        void Delete(Guid EntityID);
        void Save();
    }
}
