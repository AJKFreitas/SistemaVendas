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

        public async Task<int> Excluir(Guid id)
        {
            try
            {
                Produto produto = null;
                produto = _context.Produtos.Find(id);
                if (produto != null)
                    _context.Remove(produto);
                return await SalvarCommit();
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<PagedList<Produto>> BuscarPorFiltroComPaginacao(ProdutoParams prodParams)
        {
            try
            {
                var prodPaged =  _context.Produtos.AsQueryable();


                if (prodParams.Filter != null)
                {
                    prodPaged = prodPaged.Where(x => x.Nome.ToLower().Contains(prodParams.Filter.ToLower()) 
                    || x.Descricao.ToLower().Contains(prodParams.Filter.ToLower()) 
                    || x.Valor.ToString().ToLower().Contains(prodParams.Filter.ToLower()) 
                    || x.Codigo.ToString().ToLower().Contains(prodParams.Filter.ToLower()));
                }
                if (prodParams.SortOrder.ToLower().Equals("asc"))
                {
                    prodPaged = prodPaged.OrderBy(prod => prod.Nome);
                }
                if (prodParams.SortOrder.ToLower().Equals("desc"))
                {
                    prodPaged = prodPaged.OrderByDescending(prod => prod.Nome);
                }

                var result = await prodPaged.ToListAsync();

                return PagedList<Produto>.ToPagedList(result, prodParams.NumeroDaPaginaAtual,prodParams.TamanhoDaPagina);
               
            }
            catch (MySqlException ex)
            {
                _context.Dispose();
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Produto>> BuscarTodos()
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

        public async Task<Produto> BuscarPorId(Guid EntityID)
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

        public async Task<int> Inserir(Produto Produto)
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
                return await SalvarCommit();

            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
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
            finally
            {
                _context.Dispose();
            }
        }

        public async Task<int> Editar(Produto Produto)
        {
            try
            {
                _context.Entry(Produto).State = EntityState.Modified;
                _context.Produtos.Update(Produto);
                return await SalvarCommit();
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
