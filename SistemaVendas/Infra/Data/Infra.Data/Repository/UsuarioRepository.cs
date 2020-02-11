using Infra.Data;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Infra.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario> , IUsuarioRepository
    {
        protected readonly VendasEFContext _context;
        private bool disposed = false;
        public UsuarioRepository()
        {
            _context = new VendasEFContext();
        }

        public UsuarioRepository(VendasEFContext context)
        {
            _context = context;
        }

        public override void Delete(Guid EntityID)
        {
            var user = _context.Usuarios.Find(EntityID);
            _context.Remove(user);
        }

       

        public override IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios;
        }

        public override Usuario GetById(Guid EntityID)
        {
            return _context.Usuarios.Find(EntityID);
        }

        public override void Insert(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public override void Save()
        {
            _context.SaveChanges();
        }

        public override void Update(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
        }

        protected virtual  void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
