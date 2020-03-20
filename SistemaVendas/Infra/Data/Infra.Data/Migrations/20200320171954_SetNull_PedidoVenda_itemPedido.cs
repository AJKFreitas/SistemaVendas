using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVendas.Infra.Data.Migrations
{
    public partial class SetNull_PedidoVenda_itemPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ItemPedido_TB_Pedido_IdPedido",
                table: "TB_ItemPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_ItemPedido_TB_Produto_IdProduto",
                table: "TB_ItemPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Pedido_TB_Cliente_IdCliente",
                table: "TB_Pedido");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ItemPedido_TB_Pedido_IdPedido",
                table: "TB_ItemPedido",
                column: "IdPedido",
                principalTable: "TB_Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ItemPedido_TB_Produto_IdProduto",
                table: "TB_ItemPedido",
                column: "IdProduto",
                principalTable: "TB_Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Pedido_TB_Cliente_IdCliente",
                table: "TB_Pedido",
                column: "IdCliente",
                principalTable: "TB_Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ItemPedido_TB_Pedido_IdPedido",
                table: "TB_ItemPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_ItemPedido_TB_Produto_IdProduto",
                table: "TB_ItemPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Pedido_TB_Cliente_IdCliente",
                table: "TB_Pedido");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ItemPedido_TB_Pedido_IdPedido",
                table: "TB_ItemPedido",
                column: "IdPedido",
                principalTable: "TB_Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ItemPedido_TB_Produto_IdProduto",
                table: "TB_ItemPedido",
                column: "IdProduto",
                principalTable: "TB_Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Pedido_TB_Cliente_IdCliente",
                table: "TB_Pedido",
                column: "IdCliente",
                principalTable: "TB_Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
