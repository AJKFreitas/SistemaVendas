using SistemaVendas.Infra.Data;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository//: Repository<Usuario> , IUsuarioRepository
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

        public  void Delete(Guid EntityID)
        {
            var user = _context.Usuarios.Find(EntityID);
            _context.Remove(user);
        }

       

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return _context.Usuarios;
        }

        public async Task<Usuario> GetById(Guid EntityID)
        {
            return _context.Usuarios.Find(EntityID);
        }

        public  void Insert(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public  void Save()
        {
            _context.SaveChanges();
        }

        public  void Update(Usuario usuario)
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
        public  void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
