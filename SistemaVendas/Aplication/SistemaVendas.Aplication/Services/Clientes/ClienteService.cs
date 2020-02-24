using AutoMapper;
using SistemaVendas.Aplication.InterfaceServices.Clientes;
using SistemaVendas.Core.Domains.Auth.Entities;
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


        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
            
        }

        public async Task<int> Delete(Guid Id)
        {
            try
            {
                return await _repository.Delete(Id);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool ExisteCliente(long cpf)
        {
            try
            {
                return _repository.ExisteCliente(cpf);
            }
            catch (Exception ex)
            {

                throw new Exception (ex.Message);
            }
        }

        public async Task<IEnumerable<Cliente>> GetALL(ClienteParams clienteParams)

        {
            try
            {
                return await _repository.GetAll(clienteParams);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async  Task<IEnumerable<Cliente>> GetAll()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Cliente> GetById(Guid Id)
        {
            try
            {
                return await _repository.GetById(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public Task<int> Insert(Cliente cliente)
        {
            try
            {
                return _repository.Insert(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task<int> Update(Cliente cliente)
        {
            try
            {
                return _repository.Update(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
