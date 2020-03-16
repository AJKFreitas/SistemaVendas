using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Pedidos.Entities
{
    public class PedidoVendaParams : Parametros
    {
        public string Filter { get; set; }
        public string SortOrder { get; set; } = "asc";
    }

}
