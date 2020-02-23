using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Shared.Entities;
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



        public Task<int> Delete(Guid EntityID)
        {
            try
            {
                var usuario = _context.Usuarios.Find(EntityID);
                _context.Usuarios.Remove(usuario);
                return _context.SaveChangesAsync();
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<PagedList<Usuario>> GetAll(UsuarioParams usuarioParams)
        {
            try
            {
                var query = _context.Usuarios;   
                return  await PagedList<Usuario>.CreateAsync(query, usuarioParams.PageNumber, usuarioParams.PageSize);
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        } 
        public async Task<IEnumerable<Usuario>> GetAll()
        {
            try
            {
                return  _context.Usuarios;   
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<Usuario> GetById(Guid EntityID)
        {
            try
            {
                return await _context.Usuarios.FindAsync(EntityID);
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<int> Insert(Usuario Usuario)
        {
            try
            {
                Usuario usuario = new Usuario(
                    Usuario.Nome,
                    Usuario.Email,
                    Usuario.Senha,
                    Usuario.Role
                    );
                _context.Usuarios.Add(usuario);
                return await _context.SaveChangesAsync();

            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public Task<int> Save()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<int> Update(Usuario usuario)
        {
            try
            {
                _context.Entry(usuario).State = EntityState.Modified;
                _context.Usuarios.Update(usuario);
                return await _context.SaveChangesAsync();
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }
    }
}


