using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Fornecedores.Interfaces
{
    public interface IFornecedorRepository

    {
        Task<PagedList<Fornecedor>> BuscarPorFiltroComPaginacao(FornecedorParams parametros);
        Task<IEnumerable<Fornecedor>> BuscarTodos();
        Task<Fornecedor> BuscarPorId(Guid Id);
        Task<int> Inserir(Fornecedor fornecedor);
        Task<int> Editar(Fornecedor fornecedor);
        Task<int> Excluir(Guid Id);
        Task<int> Save();
        bool ExisteFornecedor(long cnpj);
    }
}
