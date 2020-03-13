using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Clientes.Interfaces
{
    public interface IClienteRepository
    {
        Task<PagedList<Cliente>> BuscarPorFiltroComPaginacao(ClienteParams clienteParams);
        Task<Cliente> BuscarPorId(Guid Id);
        Task<IEnumerable<Cliente>> BuscarTodos();
        Task<int> Inserir(Cliente cliente);
        Task<int> Editar(Cliente cliente);
        Task<int> Excluir(Guid Id);
        Task<int> SalvarCommit();
        bool ExisteCliente(long cpf);

    }
}
