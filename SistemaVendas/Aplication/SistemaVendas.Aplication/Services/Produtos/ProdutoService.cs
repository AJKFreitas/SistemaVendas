using AutoMapper;
using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Domains.Produtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVendas.Aplication.InterfaceServices.Produtos;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Shared.Entities;
using System.Linq;

namespace SistemaVendas.Aplication.Services.Produtos
{
  public  class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;
        public ProdutoService(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Delete(Guid Id)
        {
            try
            {
             return await _repository.Delete(Id);

            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task<PagedList<Produto>> GetAll(ProdutoParams fParams)
        {
            try
            {

                return await _repository.GetAll(fParams);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            try
            {
                IEnumerable <Produto> produtos =  await _repository.GetAll();
                produtos.Select(p => p.EstoqueAtual = CalcularEstoque(p.Id)).ToList();
                return  produtos;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private long CalcularEstoque(Guid id)
        {
            return _repository.Calcularestoque(id);
        }

        public Task<Produto> GetById(Guid Id)
        {
            try
            {
                return _repository.GetById(Id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Task<int> Insert(ProdutoVM ProdutoVM)
        {
            try
            {
                var prod = _mapper.Map<ProdutoVM, Produto>(ProdutoVM);
                return _repository.Insert(prod);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Task<int> Update(Produto Produto)
        {
            try
            {
                return _repository.Update(Produto);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool ExisteProduto(long codigo)
        {
            try
            {
                return _repository.ExisteProduto(codigo);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
