using SistemaVendas.Core.Domains.Fornecedores.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SistemaVendas.Aplication.InterfaceServices.Fornecedores
{
   public interface IFornecedorService
    {
        IEnumerable<Fornecedor> GetAll();
        Fornecedor GetById(Guid EntityID);
        HttpStatusCode Insert(Fornecedor Entity);
        void Update(Fornecedor Entity);
        void Delete(Guid EntityID);
        
    }
}
