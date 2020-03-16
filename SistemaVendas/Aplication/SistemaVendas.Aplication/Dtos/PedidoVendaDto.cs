using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.Dtos
{
  public class PedidoVendaDto
    {
        public Guid Id { get; set; }
        public DateTime DataVenda { get; set; }
        public ClienteDto Cliente { get; set; }
        public IEnumerable<ItemPedidoVendaDto> ItemPedidos { get; set; } = new List<ItemPedidoVendaDto>();
        public double ValorTotal { get; set; }

        public PedidoVendaDto()
        {

        }
    }
}
