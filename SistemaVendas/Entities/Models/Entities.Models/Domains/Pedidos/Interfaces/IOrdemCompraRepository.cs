using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Pedidos.Interfaces
{
    public interface IOrdemCompraRepository
    {
        Task<PagedList<OrdemCompra>> BuscarPorFiltroComPaginacao(OrdemCompraParams parametros);
        Task<IEnumerable<OrdemCompra>> BuscarTodos();
        Task<OrdemCompra> BuscarPorId(Guid id);
        Task<int> Inserir(OrdemCompra ordem);
        Task<int> Editar(OrdemCompra ordem);
        Task<int> Excluir(Guid id);
        Task<int> SalvarCommit();
        bool ExisteOrdemCompra(Guid id);
    }
}
