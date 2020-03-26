using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Dashboard
{
   public interface IDashBoardRepository
    {
       
        Task<IEnumerable<dynamic>> ProdutosMaisVendidos();
    }
}
