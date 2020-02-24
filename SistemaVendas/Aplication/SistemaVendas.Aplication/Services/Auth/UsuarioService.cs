using SistemaVendas.Aplication.InterfaceServices.Auth;
using SistemaVendas.Core.Domains.Auth.Entities;
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
        public async Task<int> Delete(Guid id)
        {
            try
            {
                return await _repository.Delete(id);

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

        public async Task<IEnumerable<Usuario>> GetAll(UsuarioParams uparams)
        {
            try
            {
                
                return await  _repository.GetAll(uparams);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        } 
        public async Task<IEnumerable<Usuario>> GetAll()
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

        public Task<Usuario> GetById(Guid Id)
        {
            try
            {
                return _repository.GetById(Id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task<int> Insert(Usuario usuario)
        {
            try
            {
                             
                return _repository.Insert(usuario);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

 

        public Task<int> Update(Usuario usuario)
        {
            try
            {
            return _repository.Update(usuario);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}
