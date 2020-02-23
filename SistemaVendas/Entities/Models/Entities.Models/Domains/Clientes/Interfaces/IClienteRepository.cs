using SistemaVendas.Core.Domains.Clientes.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Clientes.Interfaces
{
    public interface IClienteRepository
    {
        Task<int> Delete(Guid clienteId);
        Task<IEnumerable<Cliente>> GetAll();
        Task<Cliente> GetById(Guid clienteId);
        Task<int> Insert(Cliente cliente);
        Task<int> Save();
        Task<int> Update(Cliente cliente);
    }
}
