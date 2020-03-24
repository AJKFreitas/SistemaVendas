using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Pedidos.Interfaces
{
   public interface IPedidoVendaRepository
    {
        Task<PagedList<PedidoVenda>> BuscarPorFiltroComPaginacao(PedidoVendaParams parametros);
        Task<IEnumerable<PedidoVenda>> BuscarTodos();
        Task<PedidoVenda> BuscarPorId(Guid id);
        Task<int> Inserir(PedidoVenda pedido);
        Task<int> Editar(PedidoVenda pedido);
        Task<int> Excluir(Guid id);
        Task<int> SalvarCommit();
        bool ExistePedido(Guid id);
    }
}
