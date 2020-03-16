using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Clientes
{
   public interface IClienteService
    {
        Task<PagedList<Cliente>> BuscarPorFiltroComPaginacao(ClienteParams clienteParams);
        Task<IEnumerable<Cliente>> BuscarTodos();
        Task<Cliente> BuscarPorId(Guid Id);
        Task<int> Inserir(Cliente Cliente);
        Task<int> Editar(Cliente Cliente);
        Task<int> Excluir(Guid Id);
        bool ExisteCliente(long cpf);
    }
       
        
}
