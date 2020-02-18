using AutoMapper;
using SistemaVendas.Core.Domains.Produtos.Dtos;
using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Domains.Produtos.Interfaces;
using SistemaVendas.Core.Domains.Produtos.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Produtos.Services
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

        public Task Delete(Guid Id)
        {
            try
            {
             return _repository.Delete(Id);

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Task<IAsyncEnumerable<Produto>> GetAll()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Task<Produto> GetById(Guid Id)
        {
            try
            {
                return _repository.GetById(Id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Task<int> Insert(ProdutoVM ProdutoVM)
        {
            try
            {
                var prod = _mapper.Map<Produto>(ProdutoVM);
                return _repository.Insert(prod);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Task<int> Update(Produto Produto)
        {
            return _repository.Update(Produto);
        }
    }
}
