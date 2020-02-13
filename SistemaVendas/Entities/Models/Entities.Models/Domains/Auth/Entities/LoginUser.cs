using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Auth.Entities
{
   public sealed class LoginUser
    {
        public string LoginOrEmail { get; set; }
        public string Password { get; set; }
    }
}
