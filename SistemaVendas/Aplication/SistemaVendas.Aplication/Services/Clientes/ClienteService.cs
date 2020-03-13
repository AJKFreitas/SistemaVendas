using AutoMapper;
using SistemaVendas.Aplication.InterfaceServices.Clientes;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Domains.Clientes.Interfaces;
using SistemaVendas.Core.Shared.Entities;
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

        public async Task<int> Excluir(Guid Id)
        {
            try
            {
                return await _repository.Excluir(Id);

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

        public async Task<PagedList<Cliente>> BuscarPorFiltroComPaginacao(ClienteParams clienteParams)
        {
            try
            {
                return await _repository.BuscarPorFiltroComPaginacao(clienteParams);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        //public async Task<IEnumerable<Cliente>> GetALL(ClienteParams clienteParams)

        //{
        //    try
        //    {
        //        return await _repository.GetAll(clienteParams);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
        //}

        public async  Task<IEnumerable<Cliente>> BuscarTodos()
        {
            try
            {
                return await _repository.BuscarTodos();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Cliente> BuscarPorId(Guid Id)
        {
            try
            {
                return await _repository.BuscarPorId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public Task<int> Inserir(Cliente cliente)
        {
            try
            {
                return _repository.Inserir(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task<int> Editar(Cliente cliente)
        {
            try
            {
                return _repository.Editar(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
