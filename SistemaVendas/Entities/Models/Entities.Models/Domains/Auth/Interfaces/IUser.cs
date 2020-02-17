using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Auth.Interfaces
{
  public interface IUser<EnumTipoUsuario>
    {
     public string Id { get; set; }
     public string Nome { get; set; }
     public EnumTipoUsuario Tipo { get; }
    }
}
