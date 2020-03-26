using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Infra.Data.Repository
{
    public class DashBoardRepository : IDashBoardRepository
    {
        protected readonly VendasEFContext _context;
        IConfiguration _configuration;

        public DashBoardRepository(VendasEFContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<dynamic>> ProdutosMaisVendidos()
        {
            using (MySqlConnection conexao = new MySqlConnection(_configuration.GetConnectionString("mysqlconnectionstring")))
            {
                try
                {
                    conexao.Open();
                    var query = $"SELECT pr.Nome as Nome,  sum(ip.Quantidade) as quantidadeVendida " +
                        $"FROM db_vendas.TB_ItemPedido ip " +
                        $"inner join TB_Produto pr on pr.Id = ip.IdProduto " +
                        $"inner join TB_Pedido pd on pd.id = ip.IdPedido " +
                        $"where pd.DataVenda BETWEEN CURDATE() - INTERVAL 30 DAY AND CURDATE() + 1 " +
                        $"group by Nome order by quantidadeVendida desc limit 10";
                    return await conexao.QueryAsync<dynamic>(query);
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
