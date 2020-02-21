using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

namespace SistemaVendas.Infra.Data.Repository
{
    class PedidoRepository : IRepository<PedidoVenda>
    {
        protected readonly VendasEFContext _context;

        public PedidoRepository(VendasEFContext context)
        {
            _context = context;
        }

        public async Task<int> Delete(Guid Id)
        {
            try
            {
                var pedido = _context.Pedidos.FindAsync(Id);
                _context.Remove(pedido);
                return await Save();
            }
            catch (MySqlException e)
            {
                throw e;
            }
            finally
            {
                _context.Dispose();
            }
        }

        public async Task<IAsyncEnumerable<PedidoVenda>> GetAll()
        {
            try
            {
                return  _context.Pedidos.AsAsyncEnumerable<PedidoVenda>();
            }
            catch (MySqlException e)
            {
                throw e;
            }
            finally
            {
                _context.Dispose();
            }
        }

        public async Task<PedidoVenda> GetById(Guid Id)
        {
            try
            {
                return await _context.Pedidos.FindAsync(Id);
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
            finally
            {
                _context.Dispose();
            }
        }

        public async Task<int> Insert(PedidoVenda pedido)
        {
            try
            {
                PedidoVenda newPedido = new PedidoVenda(
                        pedido.Moment,
                        pedido.IdCliente,
                        pedido.ItemPedidos,
                        pedido.ValorTotal
                    );
                
                _context.Pedidos.Add(newPedido);
                return await Save();

            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
            finally
            {
                _context.Dispose();
            }
        }

        public async Task<int> Save()
        { 
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
            finally
            {
                _context.Dispose();
            }
        }

        public async Task<int> Update(PedidoVenda T)
        {
            try
            {
                _context.Entry(T).State = EntityState.Modified;
                _context.Pedidos.Update(T);
                return await Save();
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }
    }
}
