using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SistemaVendas.Aplication.Dtos;
using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Domains.Pedidos.Interfaces;
using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                pedidoVenda = await BuscarPorId(id);
                if (pedidoVenda != null)
                    _context.Remove(pedidoVenda);
                return await SalvarCommit();
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ExistePedido(Guid id)
        {
            PedidoVenda pedidoVenda = null;

            try
            {
                pedidoVenda = _context.Pedidos.Where(x => x.Id == id).FirstOrDefault();
                return pedidoVenda != null;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagedList<PedidoVenda>> BuscarPorFiltroComPaginacao(PedidoVendaParams parametros)
        {
            try
            {
                var pedidos = _context.Pedidos
                    .Include(p => p.Cliente)
                    .Include(p => p.ItemPedidos)
                    .ThenInclude(i => i.Produto).AsQueryable();

                
                if (parametros.Filter != null)
                {
                    pedidos = pedidos.Where(x => x.Cliente.Nome.ToLower().Contains(parametros.Filter.ToLower())
                    || x.ValorTotal.ToString().ToLower().Contains(parametros.Filter.ToLower())
                    || x.DataVenda.ToString().ToLower().Contains(parametros.Filter.ToLower())
                    );
                }
                if (parametros.SortOrder.ToLower().Equals("asc"))
                {
                    pedidos = pedidos.OrderBy(pedido => pedido.Cliente.Nome);
                }
                if (parametros.SortOrder.ToLower().Equals("desc"))
                {
                    pedidos = pedidos.OrderByDescending(pedido => pedido.Cliente.Nome);
                }

                var result = await pedidos.Select(
                    p => new PedidoVenda
                    {
                        Id = p.Id,
                        DataVenda = p.DataVenda,
                        ValorTotal = p.ValorTotal,
                        Cliente = new Cliente
                        {
                            Id = p.Cliente.Id,
                            Nome = p.Cliente.Nome,
                            CPF = p.Cliente.CPF,
                            Endereco = p.Cliente.Endereco,
                            Telefone = p.Cliente.Telefone,
                            Pedidos = new List<PedidoVenda>()
                        },
                        ItemPedidos = p.ItemPedidos.Select(ip => new ItemPedidoVenda
                        {
                            Id = ip.Id,
                            IdPedido = ip.IdPedido,
                            IdProduto = ip.IdProduto,
                            Preco = ip.Preco,
                            Quantidade = ip.Quantidade,
                            SubTotal = ip.SubTotal,
                            Produto = new Produto
                            {
                                Id = ip.Produto.Id,
                                Nome = ip.Produto.Nome,
                                Descricao = ip.Produto.Descricao,
                                Valor = ip.Produto.Valor
                            }
                        }).ToList()
                    }).ToListAsync();

                return PagedList<PedidoVenda>.ToPagedList(result, parametros.NumeroDaPaginaAtual, parametros.TamanhoDaPagina);

            }
            catch (MySqlException ex)
            {
                _context.Dispose();
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PedidoVenda>> BuscarTodos()
        {
            try
            {
                return await _context.Pedidos.ToListAsync();
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw new Exception(e.Message);
            }
        }

        public async Task<PedidoVenda> BuscarPorId(Guid Id)
        {
            try
            {
                return await _context.Pedidos.AsNoTracking().Where(p => p.Id == Id).FirstOrDefaultAsync();
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw new Exception(e.Message);
            }
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

        public async Task<int> Editar(PedidoVenda pedido)
        {
            try
            {
                PedidoVenda pedidoVenda = await BuscarPorId(pedido.Id);
                

                pedidoVenda.Id = pedido.Id;
                pedidoVenda.DataVenda = pedido.DataVenda;
                pedidoVenda.IdCliente = pedido.IdCliente;
                pedidoVenda.ItemPedidos = pedido.ItemPedidos;
                pedidoVenda.ValorTotal = pedido.ValorTotal;
               
                 
                var itemsPedidos = pedido.ItemPedidos.Select(i => new ItemPedidoVenda
                {
                    Id = i.Id,
                    Quantidade = i.Quantidade,
                    Preco = i.Preco,
                    SubTotal = i.SubTotal,
                    IdProduto = i.IdProduto,
                    IdPedido = pedidoVenda.Id
                }).ToList();
                var itensPedidosDatabase = await _context.ItemsPedidos.Where(i => i.IdPedido == pedido.Id).ToListAsync();
                var itensPedidosAPI = pedido.ItemPedidos;
                var itensPedidosParaExclusao = itensPedidosDatabase.Where(itens => !itensPedidosAPI.Contains(itens))?.ToList();
               
                itensPedidosParaExclusao.ForEach(item => _context.ItemsPedidos.Remove(item));

                pedidoVenda.ItemPedidos = itemsPedidos;
                _context.Entry(pedidoVenda).State = EntityState.Modified;
                _context.Pedidos.Update(pedidoVenda);
                return await SalvarCommit();
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
