using AutoMapper;
using Newtonsoft.Json;
using SistemaVendas.Aplication.InterfaceServices.Pedidos;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Domains.Pedidos.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int> Excluir(Guid Id)
        {
            try
            {
                return await _repository.Excluir(Id);

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool ExistePedidoVenda(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<PedidoVenda>> BuscarPorFiltroComPaginacao(PedidoVendaParams parametros)
        {
            try
            {
                return await _repository.BuscarPorFiltroComPaginacao(parametros);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<PedidoVenda>> BuscarTodos()
        {
            try
            {
                return await _repository.BuscarTodos();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Task<PedidoVenda> BuscarPorId(Guid Id)
        {
            try
            {
                return _repository.BuscarPorId(Id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<int> Inserir(LancarPedidoVendaVM pedidoVendaVM, string Token)
        {
            try
            {
                var novoPedido = _mapper.Map<LancarPedidoVendaVM, PedidoVenda>(pedidoVendaVM);
                var jwt = Token.Replace("Bearer ", string.Empty);
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadToken(jwt) as JwtSecurityToken;
                var usuarioJson = token.Claims.First(claim => claim.Type == "Data").Value;
                var usuarioLogado = JsonConvert.DeserializeObject<Usuario>(usuarioJson);
                novoPedido.IdUsuarioLogado = usuarioLogado.Id;

                return await _repository.Inserir(novoPedido);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Task<int> Editar(PedidoVendaVM pedidoVendaVM)
        {
            try
            {
                //(Guid id, DateTime dataVenda, Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal
                var itemsPedidos = new List<ItemPedidoVenda>();
                foreach (var item in pedidoVendaVM.ItemPedidosVM)
                {
                    Guid idItemPedido;

                    if (item.Id == Guid.Empty || item.Id == null)
                    {
                        idItemPedido = Guid.NewGuid();
                    }
                    else
                    {
                        idItemPedido = item.Id.GetValueOrDefault();
                    }

                    itemsPedidos.Add(new ItemPedidoVenda(idItemPedido, item.Quantidade, item.Preco, item.SubTotal, item.IdProduto, pedidoVendaVM.Id));
                }
                var novoPedido = new PedidoVenda(
                    pedidoVendaVM.Id,
                    pedidoVendaVM.DataVenda.Value,
                    pedidoVendaVM.IdCliente,
                    itemsPedidos,
                    pedidoVendaVM.ValorTotal,
                   pedidoVendaVM.IdUsuarioLogado);
                return _repository.Editar(novoPedido);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
