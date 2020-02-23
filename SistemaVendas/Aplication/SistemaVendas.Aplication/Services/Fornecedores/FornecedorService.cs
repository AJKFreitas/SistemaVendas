using SistemaVendas.Aplication.InterfaceServices.Fornecedores;
using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Domains.Fornecedores.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Net;
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

        public async Task<int> Delete(Guid EntityID)
        {
            try
            {

           return await _repository.Delete(EntityID);
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public async Task<PagedList<Fornecedor>> GetAll(FornecedorParams fParams)
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
        public async Task<IEnumerable<Fornecedor>> GetAll()
        {
            try
            {

                return await _repository.GetAll();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Task<Fornecedor> GetById(Guid Id)
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

        public Task<int> Insert(Fornecedor fornecedor)
        {
            try
            {
                
               return _repository.Insert(fornecedor);
              
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<int> Update(Fornecedor fornecedor)
        {
            try
            {
                return _repository.Update(fornecedor);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
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
