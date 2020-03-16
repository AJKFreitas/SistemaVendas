using AutoMapper;
using SistemaVendas.Aplication.InterfaceServices.Pedidos;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Domains.Pedidos.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SistemaVendas.Aplication.Dtos;
using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Domains.Produtos.Entities;

namespace SistemaVendas.Aplication.Services.Pedidos
{
    public class PedidoVendaService : IPedidoVendaService
    {

        private readonly IPedidoVendaRepository _repository;
        private readonly IMapper _mapper;

        public PedidoVendaService(IPedidoVendaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> Excluir(Guid Id)
        {
            throw new NotImplementedException();
        }

        public bool ExistePedidoVenda(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<PedidoVenda>> BuscarPorFiltroComPaginacao(PedidoVendaParams parametros)
        {
            try
            {
                var pedidos = await _repository.BuscarPorFiltroComPaginacao(parametros);
                var data =  pedidos.Select(
                    p => new PedidoVenda
                    {
                        Id = p.Id,
                        DataVenda = p.DataVenda,
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
                        }).ToList(),
                        ValorTotal = p.ValorTotal,
                    }).ToList();

                return new PagedList<PedidoVenda>(data, pedidos.Count, pedidos.CurrentPage, pedidos.PageSize);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Task<IEnumerable<PedidoVenda>> BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public Task<PedidoVenda> BuscarPorId(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Inserir(PedidoVendaVM pedidoVendaVM)
        {
            try
            {
                var novoPedido = _mapper.Map<PedidoVendaVM, PedidoVenda>(pedidoVendaVM);

                return await _repository.Inserir(novoPedido);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Task<int> Editar(PedidoVenda PedidoVenda)
        {
            throw new NotImplementedException();
        }
    }
}
