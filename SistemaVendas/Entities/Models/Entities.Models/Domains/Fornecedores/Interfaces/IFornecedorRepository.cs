using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Core.Domains.Fornecedores.Interfaces
{
    public interface IFornecedorRepository : IDisposable

    {
        public void Delete(Guid EntityID);
        public IEnumerable<Fornecedor> GetAll();
        public Fornecedor GetById(Guid EntityID);
        public void Insert(Fornecedor Entity);
        public void Save();
        public void Update(Fornecedor Entity);
    }
}
