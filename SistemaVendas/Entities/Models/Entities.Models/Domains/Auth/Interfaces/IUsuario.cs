using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Auth.Interfaces
{
  public  interface IUsuario
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

    }
}
