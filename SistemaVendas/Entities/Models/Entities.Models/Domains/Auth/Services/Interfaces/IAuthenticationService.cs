using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Auth.Services.Interfaces
{
  public  interface IAuthenticationService
    {
        Task<AuthenticationResult> AuthenticateAsync(IUsuario user);
    }
}
