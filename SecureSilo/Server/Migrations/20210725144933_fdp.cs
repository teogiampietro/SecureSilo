using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class fdp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descripcion",
                value: "-- Seleccione una forma de pago --");

            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descripcion",
                value: "Efectivo");

            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Descripcion",
                value: "Transferencia");

            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Descripcion",
                value: "Mercado Pago");

            migrationBuilder.InsertData(
                table: "FormasDePagos",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 5, "Tarjeta Credito" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descripcion",
                value: "Efectivo");

            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descripcion",
                value: "Transferencia");

            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Descripcion",
                value: "Mercado Pago");

            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Descripcion",
                value: "Tarjeta Credito");
        }
    }
}
