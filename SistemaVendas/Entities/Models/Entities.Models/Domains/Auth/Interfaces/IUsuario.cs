using System;

namespace SistemaVendas.Core.Domains.Auth.Interfaces
{
    public interface IUsuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }

    }
}
