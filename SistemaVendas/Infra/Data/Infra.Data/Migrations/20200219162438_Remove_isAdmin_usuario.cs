using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVendas.Infra.Data.Migrations
{
    public partial class Remove_isAdmin_usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoFornecedor_TB_Fornecedor_IdFornecedor",
                table: "ProdutoFornecedor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoFornecedor_TB_Produto_IdProduto",
                table: "ProdutoFornecedor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoFornecedor",
                table: "ProdutoFornecedor");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "TB_Usuario");

            migrationBuilder.RenameTable(
                name: "ProdutoFornecedor",
                newName: "ProdutosFornecidos");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoFornecedor_IdProduto",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "ProdutoFornecedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutosFornecidos_IdProduto",
                table: "ProdutoFornecedor",
                newName: "IX_ProdutoFornecedor_IdProduto");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "TB_Usuario",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoFornecedor",
                table: "ProdutoFornecedor",
                columns: new[] { "IdFornecedor", "IdProduto" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoFornecedor_TB_Fornecedor_IdFornecedor",
                table: "ProdutoFornecedor",
                column: "IdFornecedor",
                principalTable: "TB_Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoFornecedor_TB_Produto_IdProduto",
                table: "ProdutoFornecedor",
                column: "IdProduto",
                principalTable: "TB_Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
