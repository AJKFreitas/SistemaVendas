using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Services.Interfaces;
using SistemaVendas.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;

namespace SistemaVendas.Core.Domains.Auth.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
       
        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public  IEnumerable<Usuario> GetAll()
        {
            return  _repository.GetAll();
        }

        public  Usuario GetById(Guid id)
        {
            return  _repository.GetById(id);
        }

        public  HttpStatusCode Insert(Usuario usuario)
        {
            try
            {
                Usuario user = new Usuario(usuario.Nome, usuario.Email, usuario.Senha);
                _repository.Insert(user);
                return HttpStatusCode.Created;
            }
            catch (Exception e)
            {
                throw e;
                // return  HttpStatusCode.NotModified + e;
            }
        }

        public void Save()
        {
            _repository.Save();
        }

        public void Update(Usuario Entity)
        {
            _repository.Update(Entity);
        }

      
    }
}
