using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Clientes.Interfaces
{
    public interface IClienteRepository
    {
        Task<int> Delete(Guid Id);
        Task<PagedList<Cliente>> GetALL(ClienteParams clienteParams);
        Task<IEnumerable<Cliente>> GetAll(ClienteParams clienteParams);
        Task<Cliente> GetById(Guid Id);
        Task<int> Insert(Cliente cliente);
        Task<int> Save();
        Task<int> Update(Cliente cliente);
        bool ExisteCliente(long cpf);
        Task<IEnumerable<Cliente>> GetALL();
    }
}
