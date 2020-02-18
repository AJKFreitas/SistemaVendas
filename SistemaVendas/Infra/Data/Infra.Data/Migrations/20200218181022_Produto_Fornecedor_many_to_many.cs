using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVendas.Infra.Data.Migrations
{
    public partial class Produto_Fornecedor_many_to_many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoFornecedor",
                columns: table => new
                {
                    IdProduto = table.Column<Guid>(nullable: false),
                    IdFornecedor = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoFornecedor", x => new { x.IdFornecedor, x.IdProduto });
                    table.ForeignKey(
                        name: "FK_ProdutoFornecedor_TB_Fornecedor_IdFornecedor",
                        column: x => x.IdFornecedor,
                        principalTable: "TB_Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoFornecedor_TB_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "TB_Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFornecedor_IdProduto",
                table: "ProdutoFornecedor",
                column: "IdProduto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoFornecedor");

            migrationBuilder.DropTable(
                name: "TB_Produto");
        }
    }
}
