using SistemaVendas.Aplication.InterfaceServices.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.Services.DashBoard
{
    public class DashBoardService : IDashBoardService
    {
     
        Task<IEnumerable<dynamic>> IDashBoardService.ProdutosMaisVendidos()
        {
            throw new NotImplementedException();
        }
    }
}
