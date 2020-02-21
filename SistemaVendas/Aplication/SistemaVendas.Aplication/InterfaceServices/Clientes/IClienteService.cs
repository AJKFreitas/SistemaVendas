using SistemaVendas.Core.Domains.Clientes.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Clientes
{
   public interface IClienteService
    {
        Task<int> Delete(Guid Id);
        Task<IAsyncEnumerable<Cliente>> GetAll();
        Task<Cliente> GetById(Guid Id);
        Task<int> Insert(Cliente Cliente);
        Task<int> Update(Cliente Cliente);
    }
}
