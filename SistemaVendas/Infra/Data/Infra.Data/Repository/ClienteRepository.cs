using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Domains.Clientes.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        protected readonly VendasEFContext _context;
        private bool disposed = false;
        public ClienteRepository()
        {
            _context = new VendasEFContext();
        }
        
        public ClienteRepository(VendasEFContext context)
        {
            _context = context;
        }

        public async Task<int> Excluir(Guid clienteId)
        {
            try
            {
                var cliente = _context.Clientes.Find(clienteId);
                _context.Remove(cliente);
                return await SalvarCommit();
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<PagedList<Cliente>> BuscarPorFiltroComPaginacao(ClienteParams parametros)
        {
            try
            {
                var paginaClientes = _context.Clientes.AsQueryable();


                if (parametros.Filter != null)
                {
                    paginaClientes = paginaClientes.Where(x => x.Nome.ToLower().Contains(parametros.Filter.ToLower())
                    || x.Endereco.ToLower().Contains(parametros.Filter.ToLower())
                    || x.CPF.ToString().ToLower().Contains(parametros.Filter.ToLower())
                    || x.Telefone.ToLower().Contains(parametros.Filter.ToLower()));
                }
                if (parametros.SortOrder.ToLower().Equals("asc"))
                {
                    paginaClientes = paginaClientes.OrderBy(prod => prod.Nome);
                }
                if (parametros.SortOrder.ToLower().Equals("desc"))
                {
                    paginaClientes = paginaClientes.OrderByDescending(prod => prod.Nome);
                }

                var result = await paginaClientes.ToListAsync();

                return PagedList<Cliente>.ToPagedList(result, parametros.NumeroDaPaginaAtual, parametros.TamanhoDaPagina);

            }
            catch (MySqlException ex)
            {
                _context.Dispose();
                throw new Exception(ex.Message);
            }
        }
        //public async Task<PagedList<Cliente>> GetAll(ClienteParams clienteParams)
        //{
        //    try
        //    {
        //        var query = _context.Clientes;
        //        return await PagedList<Cliente>.CreateAsync(query, clienteParams.PageNumber, clienteParams.PageSize);
        //    }
        //    catch (MySqlException ex)
        //    {
        //        _context.Dispose();
        //        throw new Exception(ex.Message);

        //    }
        //}

        public async Task<IEnumerable<Cliente>> BuscarTodos()
        {
            try
            {
                return  _context.Clientes;
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<Cliente> BuscarPorId(Guid clienteId)
        {
            try
            {
                return await _context.Clientes.FindAsync(clienteId);
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<int> Inserir(Cliente cliente)
        {
            try
            {
                Cliente newCliente = new Cliente(
                    cliente.Nome,
                    cliente.CPF,
                    cliente.Telefone,
                    cliente.Endereco
                    );
                _context.Clientes.Add(newCliente);
                return await SalvarCommit();

            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
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
                _context.Dispose();
                throw new Exception(ex.Message);
            }
            finally
            {
                _context.Dispose(); 
            }
        }

        public async Task<int> Editar(Cliente cliente)
        {
            try
            {
                _context.Entry(cliente).State = EntityState.Modified;
                _context.Clientes.Update(cliente);
                return await SalvarCommit();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteCliente(long cpf)
        {
            Cliente cliente = null;

            try
            {
                cliente = _context.Clientes.Where(x => x.CPF == cpf).FirstOrDefault();
                return cliente != null;
            }
            catch ( MySqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
