using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVendas.Infra.Data.Migrations
{
    public partial class Alter_name_ProdutoFornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosFornecidos_TB_Fornecedor_IdFornecedor",
                table: "ProdutosFornecidos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosFornecidos_TB_Produto_IdProduto",
                table: "ProdutosFornecidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutosFornecidos",
                table: "ProdutosFornecidos");

            migrationBuilder.RenameTable(
                name: "ProdutosFornecidos",
                newName: "TB_Produto_Fornecedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutosFornecidos_IdProduto",
                table: "TB_Produto_Fornecedor",
                newName: "IX_TB_Produto_Fornecedor_IdProduto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_Produto_Fornecedor",
                table: "TB_Produto_Fornecedor",
                columns: new[] { "IdFornecedor", "IdProduto" });

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Produto_Fornecedor_TB_Fornecedor_IdFornecedor",
                table: "TB_Produto_Fornecedor",
                column: "IdFornecedor",
                principalTable: "TB_Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Produto_Fornecedor_TB_Produto_IdProduto",
                table: "TB_Produto_Fornecedor",
                column: "IdProduto",
                principalTable: "TB_Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Produto_Fornecedor_TB_Fornecedor_IdFornecedor",
                table: "TB_Produto_Fornecedor");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Produto_Fornecedor_TB_Produto_IdProduto",
                table: "TB_Produto_Fornecedor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_Produto_Fornecedor",
                table: "TB_Produto_Fornecedor");

            migrationBuilder.RenameTable(
                name: "TB_Produto_Fornecedor",
                newName: "ProdutosFornecidos");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Produto_Fornecedor_IdProduto",
                table: "ProdutosFornecidos",
                newName: "IX_ProdutosFornecidos_IdProduto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutosFornecidos",
                table: "ProdutosFornecidos",
                columns: new[] { "IdFornecedor", "IdProduto" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosFornecidos_TB_Fornecedor_IdFornecedor",
                table: "ProdutosFornecidos",
                column: "IdFornecedor",
                principalTable: "TB_Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosFornecidos_TB_Produto_IdProduto",
                table: "ProdutosFornecidos",
                column: "IdProduto",
                principalTable: "TB_Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
