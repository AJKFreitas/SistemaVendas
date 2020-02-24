using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Shared.Entities;
using SistemaVendas.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        protected readonly VendasEFContext _context;
        public UsuarioRepository()
        {
            _context = new VendasEFContext();
        }

        public UsuarioRepository(VendasEFContext context)
        {
            _context = context;
        }



        public async Task<int> Delete(Guid EntityID)
        {
            try
            {
                Usuario usuario = null;
                 usuario = _context.Usuarios.Find(EntityID);
                if (usuario != null)
                 _context.Usuarios.Remove(usuario);
                 return await Save();
                
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagedList<Usuario>> GetAll(UsuarioParams usuarioParams)
        {
            try
            {
                var query = _context.Usuarios;   
                return  await PagedList<Usuario>.CreateAsync(query, usuarioParams.PageNumber, usuarioParams.PageSize);
            }
            catch (MySqlException ex)
            {
                _context.Dispose();
                throw new Exception(ex.Message);
            }
        } 
        public async Task<IEnumerable<Usuario>> GetAll()
        {
            try
            {
                return  _context.Usuarios;   
            }
            catch (MySqlException ex)
            {
                _context.Dispose();
                throw new Exception(ex.Message);
            }
        }

        public async Task<Usuario> GetById(Guid EntityID)
        {
            try
            {
                return await _context.Usuarios.FindAsync(EntityID);
            }
            catch (MySqlException ex)
            {
                _context.Dispose();
                throw new Exception(ex.Message);
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
                return await Save();

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Save()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally{

                _context.Dispose();
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
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteUsuario(string email)
        {
            Usuario user = null;

            try
            {
                user = _context.Usuarios.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
                return user != null;
            }
            catch (  MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
                
        }
    }
}


