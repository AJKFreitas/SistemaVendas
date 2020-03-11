using SistemaVendas.Core.Shared.Entities;

namespace SistemaVendas.Core.Domains.Clientes.Entities
{
    public class ClienteParams : Params
    {
        public string Filter { get; set; }
        public string SortOrder { get; set; } = "asc";
    }
}
