using SistemaVendas.Core.Shared.Entities;

namespace SistemaVendas.Core.Domains.Auth.Entities
{
    public class UsuarioParams : Parametros
    {
        public string Filter { get; set; }
        public string SortOrder { get; set; } = "asc";
    }
}
