using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Domains.Clientes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        protected readonly VendasEFContext _context;

        public ClienteRepository(VendasEFContext context)
        {
            _context = context;
        }

        public async Task<int> Delete(Guid clienteId)
        {
            try
            {
                var cliente = _context.Clientes.Find(clienteId);
                _context.Remove(cliente);
                return await Save();
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<IAsyncEnumerable<Cliente>> GetAll()
        {
            try
            {
                return _context.Clientes;
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<Cliente> GetById(Guid clienteId)
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

        public async Task<int> Insert(Cliente cliente)
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
                return await Save();

            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<int> Save()
        {
            try
            {
                return await _context.SaveChangesAsync();

            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
            finally
            {
                _context.Dispose(); 
            }
        }

        public async Task<int> Update(Cliente cliente)
        {
            try
            {
                _context.Entry(cliente).State = EntityState.Modified;
                _context.Clientes.Update(cliente);
                return await Save();
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }
    }
}
