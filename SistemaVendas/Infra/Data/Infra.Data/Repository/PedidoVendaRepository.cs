using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Domains.Pedidos.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> Excluir(Guid id)
        {
            try
            {
                PedidoVenda pedidoVenda = null;
                pedidoVenda = _context.Pedidos.Find(id);
                if (pedidoVenda != null)
                    _context.Remove(pedidoVenda);
                return await SalvarCommit();
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

        public Task<PagedList<PedidoVenda>> BuscarPorFiltroComPaginacao(PedidoVendaParams produtoParams)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PedidoVenda>> BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public Task<PedidoVenda> BuscarPorId(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Inserir(PedidoVenda pedido)
        {
            try
            {
                PedidoVenda pedidoVenda = new PedidoVenda(
                    pedido.DataVenda,
                    pedido.IdCliente,
                    pedido.ItemPedidos,
                    pedido.ValorTotal
                    );
                var itemsPedidos = pedido.ItemPedidos.Select(i => new ItemPedidoVenda
                {
                    Id = Guid.NewGuid(),
                    Quantidade = i.Quantidade,
                    Preco = i.Preco,
                    SubTotal = i.SubTotal,
                    IdProduto = i.IdProduto,
                    IdPedido = pedidoVenda.Id
                }).ToList();

                pedidoVenda.ItemPedidos = itemsPedidos;

                _context.Pedidos.Add(pedidoVenda);
                return await SalvarCommit();

            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw new Exception(e.Message);
            }
        }

        public async Task<int> SalvarCommit()
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

        public Task<int> Editar(PedidoVenda produto)
        {
            throw new NotImplementedException();
        }
    }
}
