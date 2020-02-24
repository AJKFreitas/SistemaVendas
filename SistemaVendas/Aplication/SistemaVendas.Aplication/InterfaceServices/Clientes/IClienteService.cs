using SistemaVendas.Core.Domains.Clientes.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Clientes
{
   public interface IClienteService
    {

        Task<IEnumerable<Cliente>> GetALL(ClienteParams clienteParams); 
        Task<IEnumerable<Cliente>> GetAll();
        Task<Cliente> GetById(Guid Id);
        Task<int> Insert(Cliente Cliente);
        Task<int> Update(Cliente Cliente);
        bool ExisteCliente(long cpf);
        Task<int> Delete(Guid Id);
    }
       
        
}
