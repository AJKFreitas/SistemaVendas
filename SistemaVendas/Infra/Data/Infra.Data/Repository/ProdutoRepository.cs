using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Domains.Produtos.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data;

namespace SistemaVendas.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        protected readonly VendasEFContext _context;
        IConfiguration _configuration;
        public ProdutoRepository(VendasEFContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<int> Delete(Guid id)
        {
            try
            {
                Produto produto = null;
                 produto = _context.Produtos.Find(id);
                if (produto != null)
                    _context.Remove(produto);
                return await Save();
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<PagedList<Produto>> GetAll(ProdutoParams prodParams)
        {
            try
            {
                var query = _context.Produtos;
                return await PagedList<Produto>.CreateAsync(query, prodParams.PageNumber, prodParams.PageSize);
            }
            catch (MySqlException ex)
            {
                _context.Dispose();
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Produto>> GetAll()
        {
            try
            {
                return _context.Produtos;
            }
            catch (MySqlException e)
            {
                _context.Dispose();
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
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
                    Produto.Codigo,
                    Produto.ProdutoFornecedores
                    );
                _context.Produtos.Add(produto);
                return await Save();

            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
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

        public async Task<int> Update(Produto Produto)
        {
            try
            {
                _context.Entry(Produto).State = EntityState.Modified;
                _context.Produtos.Update(Produto);
                return await Save();
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ExisteProduto(long codigo)
        {
            Produto prod = null;

            try
            {
                prod = _context.Produtos.Where(x => x.Codigo == codigo).FirstOrDefault();
                return prod != null;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<dynamic> CalcularEstoque(Guid idproduto)
        {
            using (MySqlConnection conexao = new MySqlConnection(_configuration.GetConnectionString("mysqlconnectionstring")))
            {
                try
                {
                    conexao.Open();
                var query = $"SELECT (SELECT ifnull(SUM(quantidade),0) from tb_itemordemcompra where idproduto = '{idproduto}') " +
                    $"-(SELECT ifnull(SUM(quantidade), 0) from TB_ItemPedido where idproduto = '{idproduto}') estoque";
                    return await conexao.QueryFirstAsync<dynamic>(query);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    conexao.Close();
                }
            }
        }
       
    }
}
