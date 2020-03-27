using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVendas.Infra.Data.Migrations
{
    public partial class Add_Usuario_Logado_Compra_Venda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdUsuarioLogado",
                table: "TB_Pedido",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdUsuarioLogado",
                table: "TB_OrdemCompra",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TB_Pedido_IdUsuarioLogado",
                table: "TB_Pedido",
                column: "IdUsuarioLogado");

            migrationBuilder.CreateIndex(
                name: "IX_TB_OrdemCompra_IdUsuarioLogado",
                table: "TB_OrdemCompra",
                column: "IdUsuarioLogado");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_OrdemCompra_TB_Usuario_IdUsuarioLogado",
                table: "TB_OrdemCompra",
                column: "IdUsuarioLogado",
                principalTable: "TB_Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Pedido_TB_Usuario_IdUsuarioLogado",
                table: "TB_Pedido",
                column: "IdUsuarioLogado",
                principalTable: "TB_Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_OrdemCompra_TB_Usuario_IdUsuarioLogado",
                table: "TB_OrdemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Pedido_TB_Usuario_IdUsuarioLogado",
                table: "TB_Pedido");

            migrationBuilder.DropIndex(
                name: "IX_TB_Pedido_IdUsuarioLogado",
                table: "TB_Pedido");

            migrationBuilder.DropIndex(
                name: "IX_TB_OrdemCompra_IdUsuarioLogado",
                table: "TB_OrdemCompra");

            migrationBuilder.DropColumn(
                name: "IdUsuarioLogado",
                table: "TB_Pedido");

            migrationBuilder.DropColumn(
                name: "IdUsuarioLogado",
                table: "TB_OrdemCompra");
        }
    }
}
