using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVendas.Infra.Data.Migrations
{
    public partial class OrdemCompra_Produto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_OrdemCompra",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataEntrada = table.Column<DateTime>(nullable: false),
                    Nota = table.Column<string>(nullable: true),
                    IdFornecedor = table.Column<Guid>(nullable: false),
                    ValorTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_OrdemCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_OrdemCompra_TB_Fornecedor_IdFornecedor",
                        column: x => x.IdFornecedor,
                        principalTable: "TB_Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ItemOrdemCompra",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Preco = table.Column<double>(nullable: false),
                    Quantidade = table.Column<long>(nullable: false),
                    IdProduto = table.Column<Guid>(nullable: false),
                    IdOrdemCompra = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ItemOrdemCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ItemOrdemCompra_TB_OrdemCompra_IdOrdemCompra",
                        column: x => x.IdOrdemCompra,
                        principalTable: "TB_OrdemCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ItemOrdemCompra_TB_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "TB_Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ItemOrdemCompra_IdOrdemCompra",
                table: "TB_ItemOrdemCompra",
                column: "IdOrdemCompra");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ItemOrdemCompra_IdProduto",
                table: "TB_ItemOrdemCompra",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_TB_OrdemCompra_IdFornecedor",
                table: "TB_OrdemCompra",
                column: "IdFornecedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ItemOrdemCompra");

            migrationBuilder.DropTable(
                name: "TB_OrdemCompra");
        }
    }
}
