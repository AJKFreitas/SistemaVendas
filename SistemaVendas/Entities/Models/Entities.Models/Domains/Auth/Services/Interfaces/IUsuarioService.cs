using SistemaVendas.Core.Domains.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Auth.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetById(Guid EntityID);
        Task<HttpStatusCode> Insert(Usuario Entity);
        void Update(Usuario Entity);
        void Delete(Guid EntityID);
        void Save();
    }
}
