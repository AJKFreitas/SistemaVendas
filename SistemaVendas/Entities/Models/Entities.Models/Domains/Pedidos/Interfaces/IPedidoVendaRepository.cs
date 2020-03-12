using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Pedidos.Interfaces
{
   public interface IPedidoVendaRepository
    {
        Task<int> Delete(Guid Id);
        Task<PagedList<PedidoVenda>> GetAll(PedidoVendaParams produtoParams);
        Task<IEnumerable<PedidoVenda>> GetAll();
        Task<PedidoVenda> GetById(Guid Id);
        Task<int> Insert(PedidoVenda produto);
        Task<int> Save();
        Task<int> Update(PedidoVenda produto);
        bool ExistePedido(long codigo);
    }
}
