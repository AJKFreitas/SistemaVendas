using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVendas.Infra.Data.Migrations
{
    public partial class Add_Codigo_Produto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "TB_Produto",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "TB_Produto");
        }
    }
}
