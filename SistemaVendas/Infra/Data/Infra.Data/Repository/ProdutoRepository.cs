using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Domains.Produtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        protected readonly VendasEFContext _context;

        public ProdutoRepository(VendasEFContext context)
        {
            _context = context;
        }

        public Task Delete(Guid EntityID)
        {
            try
            {
                var produto = _context.Produtos.Find(EntityID);
                _context.Remove(produto);
                return _context.SaveChangesAsync();
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<IAsyncEnumerable<Produto>> GetAll()
        {
            try
            {
                return _context.Produtos;
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<Produto> GetById(Guid EntityID)
        {
            try
            {
                return await _context.Produtos.FindAsync(EntityID);
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public async Task<int> Insert(Produto Produto)
        {
            try
            {
                Produto produto = new Produto(
                    Produto.Nome,
                    Produto.Descricao,
                    Produto.Valor,
                    Produto.ProdutoFornecedores
                    );
                _context.Produtos.Add(produto);
                return await _context.SaveChangesAsync();

            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw e;
            }
        }

        public Task Save()
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

        public async Task<int> Update(Produto Produto)
        {
            try
            {
                _context.Entry(Produto).State = EntityState.Modified;
                _context.Produtos.Update(Produto);
                return await _context.SaveChangesAsync();
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }
    }
}
