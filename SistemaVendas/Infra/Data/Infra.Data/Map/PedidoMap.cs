using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVendas.Core.Domains.Pedidos.Entities;

namespace SistemaVendas.Infra.Data.Map
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasMany(pedido => pedido.ItemPedidos)
                   .WithOne(intemPedido => intemPedido.Pedido)
                   .HasForeignKey(itemPedido => itemPedido.IdPedido);

            builder.HasOne(pedido => pedido.Cliente)
                .WithMany(cliente => cliente.Pedidos)
                .HasForeignKey(pedido  => pedido.IdCliente);
        }
    }
}
