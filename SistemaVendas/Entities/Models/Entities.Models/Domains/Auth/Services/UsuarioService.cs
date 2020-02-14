using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Services.Interfaces;
using SistemaVendas.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Usuario> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<HttpStatusCode> Insert(Usuario usuario)
        {
            try
            {
                Usuario user = new Usuario(usuario.Nome, usuario.Email, usuario.Senha);
                _repository.Insert(user);
                _repository.Save();
                return HttpStatusCode.Created;
            }
            catch (Exception)
            {

                return HttpStatusCode.NotModified;
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
