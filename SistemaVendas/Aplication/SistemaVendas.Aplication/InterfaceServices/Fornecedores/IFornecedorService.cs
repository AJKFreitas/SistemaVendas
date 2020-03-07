using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Fornecedores
{
   public interface IFornecedorService
    {
        Task<List<Fornecedor>> GetAll(FornecedorParams fParams);
        Task<IEnumerable<Fornecedor>> GetAll();
        Task<Fornecedor> GetById(Guid Id);
        Task<int> Insert(Fornecedor fornecedor);
        Task<int> Update(Fornecedor fornecedor);
        bool ExisteFornecedor(long cnpj);
        Task<int> Delete(Guid Id);
    }
}
