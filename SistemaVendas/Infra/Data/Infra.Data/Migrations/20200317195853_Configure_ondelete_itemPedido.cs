using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVendas.Infra.Data.Migrations
{
    public partial class Configure_ondelete_itemPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ItemPedido_TB_Pedido_IdPedido",
                table: "TB_ItemPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ItemPedido_TB_Pedido_IdPedido",
                table: "TB_ItemPedido",
                column: "IdPedido",
                principalTable: "TB_Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ItemPedido_TB_Pedido_IdPedido",
                table: "TB_ItemPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ItemPedido_TB_Pedido_IdPedido",
                table: "TB_ItemPedido",
                column: "IdPedido",
                principalTable: "TB_Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
