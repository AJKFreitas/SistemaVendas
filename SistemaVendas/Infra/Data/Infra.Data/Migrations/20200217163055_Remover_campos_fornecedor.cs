using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVendas.Infra.Data.Migrations
{
    public partial class Remover_campos_fornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "TB_Fornecedor");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "TB_Fornecedor");

            migrationBuilder.DropColumn(
                name: "TipoUsuario",
                table: "TB_Fornecedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TB_Fornecedor",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "TB_Fornecedor",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoUsuario",
                table: "TB_Fornecedor",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
