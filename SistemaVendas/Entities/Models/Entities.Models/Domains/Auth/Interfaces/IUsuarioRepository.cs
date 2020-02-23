using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Shared.Entities;
using SistemaVendas.Core.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Infra.Data.Interfaces
{
   public interface IUsuarioRepository 
    {
        Task<int> Delete(Guid Id);
        Task<PagedList<Usuario>> GetAll(UsuarioParams usuarioParams);
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetById(Guid Id);
        Task<int> Insert(Usuario usuario);
        Task<int> Save();
        Task<int> Update(Usuario usuario);
    }
}
