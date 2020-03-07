using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Domains.Fornecedores.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Infra.Data.Repository
{
    public class FornecedorRepository : IFornecedorRepository

    {
         protected readonly VendasEFContext _context;

        public FornecedorRepository(VendasEFContext context)
        {
            _context = context;
        }

        public async Task<int> Delete(Guid EntityID)
        {
            Fornecedor fornecedor = null;
            try
            {
                 fornecedor = _context.Fornecedores.Find(EntityID);
                if (fornecedor != null)
                    _context.Remove(fornecedor);
                return await Save();
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<Fornecedor>> GetAll(FornecedorParams fornecedorParams)
        {
            try
            {
                return await _context.Fornecedores
                    .OrderBy(p => p.Nome)
                    .Skip(((fornecedorParams.PageNumber - 1) * fornecedorParams.PageSize) < 0 ? 0 : ((fornecedorParams.PageNumber - 1) * fornecedorParams.PageSize))
                    .Take(fornecedorParams.PageSize)
                    .ToListAsync();
                 //await PagedList<Fornecedor>.CreateAsync(query, fornecedorParams.PageNumber, fornecedorParams.PageSize);
            }
            catch (MySqlException ex)
            {
                _context.Dispose();
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Fornecedor>> GetAll()
        {
            
            try
            {
                return  _context.Fornecedores;
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw new Exception(e.Message);
            }
        }

        public async Task<Fornecedor> GetById(Guid EntityID)
        {
            try
            {
                return await _context.Fornecedores.FindAsync(EntityID);
            }
            catch (MySqlException ex)
            {
                _context.Dispose();
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Insert(Fornecedor fornecedor)
        {
            try
            {
                Fornecedor newFornecedor = new Fornecedor(
                    fornecedor.Nome,
                    fornecedor.Telefone,
                    fornecedor.CNPJ
                    );
                _context.Fornecedores.Add(newFornecedor);
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
            finally
            {
                _context.Dispose();
            }
        }

        public async Task<int> Update(Fornecedor fornecedor)
        {
            try
            {
                _context.Entry(fornecedor).State = EntityState.Modified;
                _context.Fornecedores.Update(fornecedor);
                return await Save();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteFornecedor(long cnpj)
        {
            Fornecedor user = null;

            try
            {
                user = _context.Fornecedores.Where(x => x.CNPJ == cnpj).FirstOrDefault();
                return user != null;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
