using SistemaVendas.Core.Domains.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Auth
{
    public interface IUsuarioService
    {
       
        Task<IEnumerable<Usuario>> GetAll(UsuarioParams uparams);
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetById(Guid Id);
        Task<int> Insert(Usuario Usuario);
        Task<int> Update(Usuario Usuario);
        bool ExisteUsuario(string email);
        Task<int> Delete(Guid Id);
    }
}
