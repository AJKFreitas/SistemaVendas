using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVendas.Infra.Data.Migrations
{
    public partial class Add_Pedido_ItemPedido_Cliente_Produto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Pedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Moment = table.Column<DateTime>(nullable: false),
                    IdCliente = table.Column<Guid>(nullable: false),
                    ValorTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Pedido_TB_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "TB_Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ItemPedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Quantidade = table.Column<long>(nullable: false),
                    Preco = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    IdProduto = table.Column<Guid>(nullable: false),
                    IdPedido = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ItemPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ItemPedido_TB_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "TB_Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ItemPedido_TB_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "TB_Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ItemPedido_IdPedido",
                table: "TB_ItemPedido",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ItemPedido_IdProduto",
                table: "TB_ItemPedido",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Pedido_IdCliente",
                table: "TB_Pedido",
                column: "IdCliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ItemPedido");

            migrationBuilder.DropTable(
                name: "TB_Pedido");
        }
    }
}
