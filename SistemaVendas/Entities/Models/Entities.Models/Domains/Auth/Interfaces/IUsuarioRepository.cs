using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Infra.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<PagedList<Usuario>> BuscarPorFiltroComPaginacao(UsuarioParams parametros);
        Task<Usuario> BuscarPorId(Guid Id);
        Task<IEnumerable<Usuario>> BuscarTodos();
        Task<int> Inserir(Usuario usuario);
        Task<int> Editar(Usuario usuario);
        Task<int> Excluir(Guid Id);
        Task<int> SalvarCommit();
        bool ExisteUsuario(string email);
    }
}
