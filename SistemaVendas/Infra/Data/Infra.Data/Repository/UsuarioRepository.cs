using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SistemaVendas.Aplication.Services.Auth;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Shared.Entities;
using SistemaVendas.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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



        public async Task<int> Excluir(Guid IdUsuario)
        {
            try
            {
                Usuario usuario = null;
                 usuario = _context.Usuarios.Find(IdUsuario);
                if (usuario != null)
                 _context.Usuarios.Remove(usuario);
                 return await SalvarCommit();
                
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<PagedList<Usuario>> BuscarPorFiltroComPaginacao(UsuarioParams parametros)
        {
            try
            {
                var prodPaged = _context.Usuarios.AsQueryable();


                if (parametros.Filter != null)
                {
                    prodPaged = prodPaged.Where(x => x.Nome.ToLower().Contains(parametros.Filter.ToLower())
                    || x.Email.ToLower().Contains(parametros.Filter.ToLower())
                    || x.Role.ToLower().Contains(parametros.Filter.ToLower()));
                }
                if (parametros.SortOrder.ToLower().Equals("asc"))
                {
                    prodPaged = prodPaged.OrderBy(prod => prod.Nome);
                }
                if (parametros.SortOrder.ToLower().Equals("desc"))
                {
                    prodPaged = prodPaged.OrderByDescending(prod => prod.Nome);
                }

                var result = await prodPaged.ToListAsync();

                return PagedList<Usuario>.ToPagedList(result, parametros.NumeroDaPaginaAtual, parametros.TamanhoDaPagina);

            }
            catch (MySqlException ex)
            {
                _context.Dispose();
                throw new Exception(ex.Message);
            }
        }
       public async Task<IEnumerable<Usuario>> BuscarTodos()
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

        public async Task<Usuario> BuscarPorId(Guid EntityID)
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

        public async Task<int> Inserir(Usuario Usuario)
        {
            var hash = new CriptografiaHash(SHA512.Create());
            try
            {


                Usuario newUsuario = new Usuario(
                        Usuario.Nome,
                        Usuario.Email,
                        hash.CriptografarSenha(Usuario.Senha),
                        Usuario.Role
                    );
                _context.Usuarios.Add(newUsuario);
                return await SalvarCommit();

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> SalvarCommit()
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

        public async Task<int> Editar(Usuario usuario)
        {
            var hash = new CriptografiaHash(SHA512.Create());
            try
            {
                 usuario.Senha = hash.CriptografarSenha(usuario.Senha);
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


