using AutoMapper;
using SistemaVendas.Aplication.InterfaceServices.Clientes;
using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Domains.Clientes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.Services.Clientes
{
    public class ClienteService : IClienteService

    {

        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository repository, IMapper mapper)
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

        public Task<IAsyncEnumerable<Cliente>> GetAll()
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

        public Task<Cliente> GetById(Guid Id)
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

        public Task<int> Insert(Cliente Cliente)
        {
            try
            {
                return _repository.Insert(Cliente);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Task<int> Update(Cliente Cliente)
        {
            return _repository.Update(Cliente);
        }
    }
}
