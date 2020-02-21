using SistemaVendas.Aplication.InterfaceServices.Auth;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace SistemaVendas.Aplication.Services.Auth
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
                Usuario exitente = GetAll().Where(u => u.Email.ToLower().Equals(usuario.Email.ToLower())).FirstOrDefault();
                if (exitente != null)
                {
                    return HttpStatusCode.Locked;
                }else
                {
                    Usuario user = new Usuario(usuario.Nome, usuario.Email, usuario.Senha, usuario.Role);
                    _repository.Insert(user);
                    return HttpStatusCode.Created;
                }

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
