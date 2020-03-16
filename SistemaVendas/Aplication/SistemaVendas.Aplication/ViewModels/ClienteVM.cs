using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.ViewModels
{
    public class ClienteVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public long CPF { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public IEnumerable<PedidoVendaVM> Pedidos { get; set; } = new List<PedidoVendaVM>();
    }
}
