using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Pedidos
{
  public  interface IPedidoVendaService
    {
        Task<PagedList<PedidoVenda>> GetAll(PedidoVendaParams pParams);
        Task<IEnumerable<PedidoVenda>> GetAll();
        Task<PedidoVenda> GetById(Guid Id);
        Task<int> Insert(PedidoVendaVM PedidoVenda);
        Task<int> Update(PedidoVenda PedidoVenda);
        bool ExisteProduto(long codigo);
        Task<int> Delete(Guid Id);
      
    }
}
