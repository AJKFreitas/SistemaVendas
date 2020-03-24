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
    public class OrdemCompraService : IOrdemCompraService
    {
        private readonly IOrdemCompraRepository _repository;
        private readonly IMapper _mapper;

        public OrdemCompraService(IOrdemCompraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedList<OrdemCompra>> BuscarPorFiltroComPaginacao(OrdemCompraParams parametros)
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

        public async Task<OrdemCompra> BuscarPorId(Guid id)
        {
            try
            {
                return await _repository.BuscarPorId(id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<OrdemCompra>> BuscarTodos()
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

        public async Task<int> Editar(OrdemCompraVM ordemVM)
        {
            try
            {
                var itemsOrdem = new List<ItemOrdemCompra>();
                foreach (var item in ordemVM.ItemsOrdemCompraVM)
                {
                    Guid idItemOrdemCompra;

                    if (item.Id == Guid.Empty || item.Id == null)
                    {
                        idItemOrdemCompra = Guid.NewGuid();
                    }
                    else
                    {
                        idItemOrdemCompra = item.Id.GetValueOrDefault();
                    }

                    itemsOrdem.Add(new ItemOrdemCompra(idItemOrdemCompra, item.Quantidade, item.Preco, item.SubTotal, item.IdProduto, ordemVM.Id));
                }
                var ordemParaEdicao = new OrdemCompra(ordemVM.Id, ordemVM.DataEntrada, ordemVM.IdFornecedor, itemsOrdem, ordemVM.ValorTotal);
                return await _repository.Editar(ordemParaEdicao);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<int> Excluir(Guid id)
        {
            try
            {
                return await _repository.Excluir(id);

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<int> Inserir(LancarOrdemCompraVM lancarOrdemVM)
        {
            try
            {
                var novaOrdem = _mapper.Map<LancarOrdemCompraVM, OrdemCompra>(lancarOrdemVM);

                return await _repository.Inserir(novaOrdem);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
