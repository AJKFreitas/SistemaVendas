using AutoMapper;
using SistemaVendas.Aplication.InterfaceServices.Pedidos;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Domains.Pedidos.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
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

        public Task<int> Editar(PedidoVendaVM pedidoVendaVM)
        {
            try
            {
                var novoPedido = _mapper.Map<PedidoVendaVM, PedidoVenda>(pedidoVendaVM);
                return _repository.Editar(novoPedido);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
