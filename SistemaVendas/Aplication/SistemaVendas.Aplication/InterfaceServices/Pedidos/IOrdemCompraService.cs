using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Pedidos
{
    public interface IOrdemCompraService
    {
        Task<PagedList<OrdemCompra>> BuscarPorFiltroComPaginacao(OrdemCompraParams parametros);
        Task<IEnumerable<OrdemCompra>> BuscarTodos();
        Task<OrdemCompra> BuscarPorId(Guid id);
        Task<int> Inserir(LancarOrdemCompraVM lancarOrdemVM);
        Task<int> Editar(OrdemCompraVM ordemVM);
        Task<int> Excluir(Guid id);
    }
}
