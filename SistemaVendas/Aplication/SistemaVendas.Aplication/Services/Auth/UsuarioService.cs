using SistemaVendas.Aplication.InterfaceServices.Auth;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Shared.Entities;
using SistemaVendas.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.Services.Auth
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
       
        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Excluir(Guid id)
        {
            try
            {
                return await _repository.Excluir(id);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool ExisteUsuario(string email)
        {
            try
            {
            return _repository.ExisteUsuario(email);

            }
            catch (Exception ex)
            {

                throw new Exception (ex.Message);
            }
        }
        public async Task<PagedList<Usuario>> BuscarPorFiltroComPaginacao(UsuarioParams userParams)
        {
            try
            {
                return await _repository.BuscarPorFiltroComPaginacao(userParams);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<IEnumerable<Usuario>> BuscarTodos()
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

        public Task<Usuario> BuscarPorId(Guid Id)
        {
            try
            {
                return _repository.BuscarPorId(Id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task<int> Inserir(Usuario usuario)
        {
            try
            {
                             
                return _repository.Inserir(usuario);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

 

        public Task<int> Editar(Usuario usuario)
        {
            try
            {
            return _repository.Editar(usuario);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}
