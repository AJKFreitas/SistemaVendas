using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Pedidos
{
  public  interface IPedidoVendaService
    {
        Task<PagedList<PedidoVenda>> BuscarPorFiltroComPaginacao(PedidoVendaParams parametros);
        Task<IEnumerable<PedidoVenda>> BuscarTodos();
        Task<PedidoVenda> BuscarPorId(Guid id);
        Task<int> Inserir(LancarPedidoVendaVM pedidoVM);
        Task<int> Editar(PedidoVendaVM pedidoVM);
        Task<int> Excluir(Guid id);
        bool ExistePedidoVenda(Guid id);
      
    }
}
