using SistemaVendas.Aplication.InterfaceServices.Pedidos;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.Services.Pedidos
{
    public class PedidoVendaService : IPedidoVendaService
    {
      

        public Task<int> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public bool ExisteProduto(long codigo)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<PedidoVenda>> GetAll(PedidoVendaParams pParams)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PedidoVenda>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PedidoVenda> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(PedidoVendaVM PedidoVenda)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(PedidoVenda PedidoVenda)
        {
            throw new NotImplementedException();
        }
    }
}
