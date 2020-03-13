using SistemaVendas.Aplication.InterfaceServices.Fornecedores;
using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Domains.Fornecedores.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.Services.Fornecedores
{
    public class FornecedorService : IFornecedorService
    {

        private readonly IFornecedorRepository _repository;

        public FornecedorService(IFornecedorRepository repository)
        {
            _repository = repository;
        }
        public async Task<PagedList<Fornecedor>> BuscarPorFiltroComPaginacao(FornecedorParams prodParams)
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
        public async Task<IEnumerable<Fornecedor>> BuscarTodos()
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

        public Task<Fornecedor> BuscarPorId(Guid Id)
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



        public async Task<int> Inserir(Fornecedor fornecedor)
        {
            try
            {
                
               return await _repository.Inserir(fornecedor);
              
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<int> Editar(Fornecedor fornecedor)
        {
            try
            {
                return _repository.Editar(fornecedor);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<int> Excluir(Guid EntityID)
        {
            try
            {

                return await _repository.Excluir(EntityID);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public bool ExisteFornecedor(long cnpj)
        {
            try
            {
                return _repository.ExisteFornecedor(cnpj);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        
    }
}
