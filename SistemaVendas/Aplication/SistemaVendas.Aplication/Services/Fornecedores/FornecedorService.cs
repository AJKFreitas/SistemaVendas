using SistemaVendas.Aplication.InterfaceServices.Fornecedores;
using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Domains.Fornecedores.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;

namespace SistemaVendas.Aplication.Services.Fornecedores
{
    public class FornecedorService : IFornecedorService
    {

        private readonly IFornecedorRepository _repository;

        public FornecedorService(IFornecedorRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Guid EntityID)
        {
            _repository.Delete(EntityID);
        }

        public IEnumerable<Fornecedor> GetAll()
        {
           return _repository.GetAll();
        }

        public Fornecedor GetById(Guid EntityID)
        {
          return  _repository.GetById(EntityID);
        }

        public HttpStatusCode Insert(Fornecedor Entity)
        {
            try
            {
                Fornecedor fornecedor = new Fornecedor(Entity.Nome, Entity.Telefone, Entity.CNPJ );
                _repository.Insert(fornecedor);
                return HttpStatusCode.Created;
            }
            catch (Exception e)
            {
                throw e;
              //  return  HttpStatusCode.NotModified;
            }
        }

        public void Save()
        {
            _repository.Save();
        }

        public void Update(Fornecedor Entity)
        {
            _repository.Update(Entity);
        }
    }
}
