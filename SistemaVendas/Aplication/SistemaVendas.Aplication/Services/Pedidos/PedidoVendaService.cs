using AutoMapper;
using SistemaVendas.Aplication.InterfaceServices.Pedidos;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Domains.Pedidos.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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

        public Task<int> Excluir(Guid Id)
        {
            throw new NotImplementedException();
        }

        public bool ExistePedidoVenda(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<PedidoVenda>> BuscarPorFiltroComPaginacao(PedidoVendaParams pParams)
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

        public async Task<int> Inserir(PedidoVendaVM pedidoVendaVM)
        {
            try
            {
                var novoPedido = _mapper.Map<PedidoVenda>(pedidoVendaVM);
                return  await  _repository.Inserir(novoPedido);
                
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
