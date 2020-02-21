using SistemaVendas.Core.Domains.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Net;

namespace SistemaVendas.Aplication.InterfaceServices.Auth
{
    public interface IUsuarioService
    {
        IEnumerable<Usuario> GetAll();
        Usuario GetById(Guid EntityID);
        HttpStatusCode Insert(Usuario Entity);
        void Update(Usuario Entity);
        void Delete(Guid EntityID);
        void Save();
    }
}
