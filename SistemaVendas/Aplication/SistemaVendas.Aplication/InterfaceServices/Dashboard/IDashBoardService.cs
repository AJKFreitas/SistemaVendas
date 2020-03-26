using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Dashboard
{
   public interface IDashBoardService
    {
        Task<IEnumerable<dynamic>> ProdutosMaisVendidos();
    }
}
