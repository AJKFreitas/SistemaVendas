using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Auth
{
    public interface IUsuarioService
    {
        Task<PagedList<Usuario>> BuscarPorFiltroComPaginacao(UsuarioParams parametros);
        Task<Usuario> BuscarPorId(Guid Id);
        Task<IEnumerable<Usuario>> BuscarTodos();
        Task<int> Inserir(Usuario Usuario);
        Task<int> Editar(Usuario Usuario);
        Task<int> Excluir(Guid Id);
        bool ExisteUsuario(string email);
    }
}
