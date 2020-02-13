using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Auth.Interfaces
{
  public interface IUser
    {
     public string Id { get; set; }
     public string Nome { get; set; }
    }
}
