using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class fdp2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Descripcion",
                value: "Tarjeta de Credito");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Descripcion",
                value: "Tarjeta Credito");
        }
    }
}
