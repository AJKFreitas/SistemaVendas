using AutoMapper;
using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Domains.Produtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVendas.Aplication.InterfaceServices.Produtos;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Shared.Entities;
using Microsoft.EntityFrameworkCore;

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
        public async Task<PagedList<Produto>> BuscarPorFiltroComPaginacao(ProdutoParams prodParams)
        {
            try
            {
               return await _repository.BuscarPorFiltroComPaginacao(prodParams);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Produto>> BuscarTodos()
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

        public async Task<dynamic> CalcularEstoque(Guid id)
        {
            try
            {
                return await _repository.CalcularEstoque(id);

            }
            catch (Exception)
            {

                throw new Exception("Erro ao tentar calcular estoque.");
            }
        }

        public Task<Produto> BuscarPorId(Guid Id)
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

        public Task<int> Inserir(ProdutoVM produtoVM)
        {
            try
            {
                var prod = _mapper.Map<ProdutoVM, Produto>(produtoVM);
                return _repository.Inserir(prod);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Task<int> Editar(Produto Produto)
        {
            try
            {
                return _repository.Editar(Produto);

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
