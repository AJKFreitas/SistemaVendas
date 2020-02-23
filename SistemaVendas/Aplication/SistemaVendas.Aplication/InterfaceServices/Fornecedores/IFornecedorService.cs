using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Fornecedores
{
   public interface IFornecedorService
    {
        IEnumerable<Fornecedor> GetAll();
        Task<PagedList<Fornecedor>> GetAll(Params usuarioParams);
        Fornecedor GetById(Guid EntityID);
        HttpStatusCode Insert(Fornecedor Entity);
        void Update(Fornecedor Entity);
        void Delete(Guid EntityID);
      

    }
}
