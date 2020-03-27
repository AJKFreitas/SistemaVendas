using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Domains.Pedidos.Interfaces;
using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Infra.Data.Repository
{
    public class OrdemCompraRepository : IOrdemCompraRepository
    {
        protected readonly VendasEFContext _context;

        public OrdemCompraRepository(VendasEFContext context)
        {
            _context = context;
        }

        public async Task<PagedList<OrdemCompra>> BuscarPorFiltroComPaginacao(OrdemCompraParams parametros)
        {
            try
            {
                var ordens = _context.OrdemCompras
                    .Include(ordem => ordem.Fornecedor)
                    .Include(ordem => ordem.ItemsOrdemCompra)
                    .ThenInclude(i => i.Produto).AsQueryable();


                if (parametros.Filter != null)
                {
                    ordens = ordens.Where(x =>
                       x.Fornecedor.Nome.ToLower().Contains(parametros.Filter.ToLower())
                    || x.ValorTotal.ToString().ToLower().Contains(parametros.Filter.ToLower())
                    || x.DataEntrada.ToString().ToLower().Contains(parametros.Filter.ToLower())
                    );
                }
                if (parametros.SortOrder.ToLower().Equals("asc"))
                {
                    if (parametros.OrdenarPor.ToLower().Equals("fornecedor"))
                    {
                        ordens = ordens.OrderBy(ordem => ordem.Fornecedor.Nome);
                    }
                    if (parametros.OrdenarPor.ToLower().Equals("dataEntrada"))
                    {
                        ordens = ordens.OrderBy(ordem => ordem.DataEntrada);
                    }
                    if (parametros.OrdenarPor.ToLower().Equals("valorTotal"))
                    {
                        ordens = ordens.OrderBy(ordem => ordem.ValorTotal);
                    }
                }
                if (parametros.SortOrder.ToLower().Equals("desc"))
                {
                    if (parametros.OrdenarPor.Equals("fornecedor"))
                    {
                        ordens = ordens.OrderByDescending(ordem => ordem.Fornecedor.Nome);
                    }
                    if (parametros.OrdenarPor.Equals("dataEntrada"))
                    {
                        ordens = ordens.OrderByDescending(ordem => ordem.DataEntrada);
                    }
                    if (parametros.OrdenarPor.Equals("valorTotal"))
                    {
                        ordens = ordens.OrderByDescending(ordem => ordem.ValorTotal);
                    }
                }

                var result = await ordens.Select(
                    ordem => new OrdemCompra
                    {
                        Id = ordem.Id,
                        DataEntrada = ordem.DataEntrada,
                        ValorTotal = ordem.ValorTotal,
                        Fornecedor = new Fornecedor
                        {
                            Id = ordem.Fornecedor.Id,
                            Nome = ordem.Fornecedor.Nome,
                            CNPJ = ordem.Fornecedor.CNPJ,
                            Telefone = ordem.Fornecedor.Telefone,
                            OrdemCompras = new List<OrdemCompra>()
                        },
                        ItemsOrdemCompra = ordem.ItemsOrdemCompra.Select(ip => new ItemOrdemCompra
                        {
                            Id = ip.Id,
                            IdOrdemCompra = ip.IdOrdemCompra,
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

                return PagedList<OrdemCompra>.ToPagedList(result, parametros.NumeroDaPaginaAtual, parametros.TamanhoDaPagina);

            }
            catch (MySqlException ex)
            {
                _context.Dispose();
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrdemCompra> BuscarPorId(Guid id)
        {
            try
            {
                return await _context.OrdemCompras.AsNoTracking()
                    .Include(p => p.ItemsOrdemCompra)
                    .ThenInclude(i => i.Produto)
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<OrdemCompra>> BuscarTodos()
        {
            try
            {
                return await _context.OrdemCompras.ToListAsync();
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw new Exception(e.Message);
            }
        }

        public async Task<int> Editar(OrdemCompra ordem)
        {
            try
            {
                OrdemCompra ordemCompra = await BuscarPorId(ordem.Id);
                var ItensDaTela = ordem.ItemsOrdemCompra.ToList();
                var ItensDoBanco = ordemCompra.ItemsOrdemCompra.ToList();

                var ItensParaRemover = ItensDoBanco.Where(itemBanco => !ItensDaTela.Exists(itemTela => itemTela.Id == itemBanco.Id)).ToList();
                if (ItensParaRemover.Count() > 0)
                {
                    foreach (var item in ItensParaRemover)
                    {
                        _context.ItemOrdemCompras.Remove(item);
                    }
                }

                var ItensParaAtualizar = ItensDoBanco.Where(itemBanco => ItensDaTela.Exists(itemTela => itemTela.Id == itemBanco.Id)).ToList();
                if (ItensParaAtualizar.Count() > 0)
                {
                    foreach (var item in ItensParaAtualizar)
                    {
                        ItensDaTela.ForEach(it =>
                        {
                            if (it.Id == item.Id)
                            {
                                item.Preco = it.Preco;
                                item.OrdemCompra = it.OrdemCompra;
                                item.Produto = it.Produto;
                                item.SubTotal = it.SubTotal;
                                item.IdOrdemCompra = it.IdOrdemCompra;
                                item.IdProduto = it.IdProduto;
                                item.Quantidade = it.Quantidade;
                                _context.ItemOrdemCompras.Update(item);
                            }
                        });
                    }
                }

                var ItensParaAdicionar = ItensDaTela.Where(itemTela => !ItensDoBanco.Exists(itemBanco => itemBanco.Id == itemTela.Id)).ToList();
                if (ItensParaAdicionar.Count() > 0)
                {
                    foreach (var item in ItensParaAdicionar)
                    {
                        await _context.ItemOrdemCompras.AddAsync(item);
                    }
                }


                ordemCompra.Id = ordem.Id;
                ordemCompra.DataEntrada = ordem.DataEntrada;
                ordemCompra.IdFornecedor = ordem.IdFornecedor;
                ordemCompra.ItemsOrdemCompra = ordem.ItemsOrdemCompra;
                ordemCompra.ValorTotal = ordem.ValorTotal;

                _context.Entry(ordemCompra).State = EntityState.Modified;
                _context.OrdemCompras.Update(ordemCompra);
                return await SalvarCommit();
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> Excluir(Guid id)
        {
            try
            {
                OrdemCompra ordemCompra = null;
                ordemCompra = await BuscarPorId(id);
                if (ordemCompra != null)
                    _context.Remove(ordemCompra);
                return await SalvarCommit();
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ExisteOrdemCompra(Guid id)
        {
            OrdemCompra ordemCompra = null;

            try
            {
                ordemCompra = _context.OrdemCompras.Where(x => x.Id == id).FirstOrDefault();
                return ordemCompra != null;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Inserir(OrdemCompra ordem)
        {
            try
            {
                OrdemCompra ordemCompra = new OrdemCompra(
                    ordem.DataEntrada,
                    ordem.IdFornecedor,
                    ordem.ItemsOrdemCompra,
                    ordem.ValorTotal,
                    ordem.IdUsuarioLogado
                    );
                var itemsOrdem = ordem.ItemsOrdemCompra.Select(i => new ItemOrdemCompra
                {
                    Id = Guid.NewGuid(),
                    Quantidade = i.Quantidade,
                    Preco = i.Preco,
                    SubTotal = i.SubTotal,
                    IdProduto = i.IdProduto,
                    IdOrdemCompra = ordemCompra.Id
                }).ToList();

                ordemCompra.ItemsOrdemCompra = itemsOrdem;

                _context.OrdemCompras.Add(ordemCompra);
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
    }
}
