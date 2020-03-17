using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVendas.Infra.Data.Migrations
{
    public partial class Configure_ondelete_Pedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ItemOrdemCompra_TB_Produto_IdProduto",
                table: "TB_ItemOrdemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_ItemPedido_TB_Produto_IdProduto",
                table: "TB_ItemPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ItemOrdemCompra_TB_Produto_IdProduto",
                table: "TB_ItemOrdemCompra",
                column: "IdProduto",
                principalTable: "TB_Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ItemPedido_TB_Produto_IdProduto",
                table: "TB_ItemPedido",
                column: "IdProduto",
                principalTable: "TB_Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ItemOrdemCompra_TB_Produto_IdProduto",
                table: "TB_ItemOrdemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_ItemPedido_TB_Produto_IdProduto",
                table: "TB_ItemPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ItemOrdemCompra_TB_Produto_IdProduto",
                table: "TB_ItemOrdemCompra",
                column: "IdProduto",
                principalTable: "TB_Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ItemPedido_TB_Produto_IdProduto",
                table: "TB_ItemPedido",
                column: "IdProduto",
                principalTable: "TB_Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
