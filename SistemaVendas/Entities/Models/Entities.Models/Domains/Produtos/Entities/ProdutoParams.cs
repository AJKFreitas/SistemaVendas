using SistemaVendas.Core.Shared.Entities;

namespace SistemaVendas.Core.Domains.Produtos.Entities
{
    public class ProdutoParams : Parametros
    {
        public string Filter { get; set; }
        public string SortOrder { get; set; } = "asc";
    }
}
