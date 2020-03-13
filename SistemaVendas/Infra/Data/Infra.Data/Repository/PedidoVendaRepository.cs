using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Domains.Pedidos.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Infra.Data.Repository
{
    public class PedidoVendaRepository : IPedidoVendaRepository
    {
        protected readonly VendasEFContext _context;

        public PedidoVendaRepository(VendasEFContext context)
        {
            _context = context;
        }

        public async Task<int> Delete(Guid id)
        {
            try
            {
                PedidoVenda pedidoVenda = null;
                pedidoVenda = _context.Pedidos.Find(id);
                if (pedidoVenda != null)
                    _context.Remove(pedidoVenda);
                return await Save();
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ExistePedido(long codigo)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<PedidoVenda>> GetAll(PedidoVendaParams produtoParams)
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

        public async Task<int> Insert(PedidoVenda pedido)
        {
            try
            {
                PedidoVenda pedidoVenda = new PedidoVenda(
                    pedido.Moment,
                    pedido.IdCliente,
                    pedido.ItemPedidos,
                    pedido.ValorTotal
                    );
                _context.Pedidos.Add(pedidoVenda);
                return await Save();

            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> Save()
        {
            try
            {
                return await _context.SaveChangesAsync();

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _context.Dispose();
            }
        }

        public Task<int> Update(PedidoVenda produto)
        {
            throw new NotImplementedException();
        }
    }
}
