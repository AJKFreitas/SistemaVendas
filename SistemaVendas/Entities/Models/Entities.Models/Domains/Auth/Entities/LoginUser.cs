using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Auth.Entities
{
   public sealed class LoginUser
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
