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
        public UsuarioService()
        {

        }
        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _repository.GetAll();
        }

        public Usuario GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Insert(Usuario usuario)
        {
            _repository.Insert(usuario);
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
