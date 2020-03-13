using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Aplication.InterfaceServices.Fornecedores
{
   public interface IFornecedorService
    {
        Task<PagedList<Fornecedor>> BuscarPorFiltroComPaginacao(FornecedorParams fParams);
        Task<IEnumerable<Fornecedor>> BuscarTodos();
        Task<Fornecedor> BuscarPorId(Guid Id);
        Task<int> Inserir(Fornecedor fornecedor);
        Task<int> Editar(Fornecedor fornecedor);
        Task<int> Excluir(Guid Id);
        bool ExisteFornecedor(long cnpj);
    }
}
