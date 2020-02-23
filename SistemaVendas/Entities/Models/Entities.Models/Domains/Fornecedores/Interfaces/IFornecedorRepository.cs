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
        Task<PagedList<Fornecedor>> GetAll(FornecedorParams usuarioParams);
        Task<IEnumerable<Fornecedor>> GetAll();
        Task<Fornecedor> GetById(Guid Id);
        Task<int> Insert(Fornecedor usuario);
        Task<int> Save();
        Task<int> Update(Fornecedor usuario);
        bool ExisteFornecedor(long cnpj);
    }
}
