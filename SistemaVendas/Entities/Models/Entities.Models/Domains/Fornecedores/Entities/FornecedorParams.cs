using SistemaVendas.Core.Shared.Entities;

namespace SistemaVendas.Core.Domains.Fornecedores.Entities
{
    public class FornecedorParams : Parametros
    {
        public string Filter { get; set; }
        public string SortOrder { get; set; } = "asc";
    }
}
