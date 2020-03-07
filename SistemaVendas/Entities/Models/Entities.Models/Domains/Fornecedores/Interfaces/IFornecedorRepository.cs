using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Fornecedores.Interfaces
{
    public interface IFornecedorRepository

    {
        Task<int> Delete(Guid Id);
        Task<List<Fornecedor>> GetAll(FornecedorParams fornecedorParams);
        Task<IEnumerable<Fornecedor>> GetAll();
        Task<Fornecedor> GetById(Guid Id);
        Task<int> Insert(Fornecedor fornecedor);
        Task<int> Save();
        Task<int> Update(Fornecedor fornecedor);
        bool ExisteFornecedor(long cnpj);
    }
}
