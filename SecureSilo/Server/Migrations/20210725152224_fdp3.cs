using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class fdp3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "FormasDePagos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CBU",
                table: "FormasDePagos",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Alias", "CBU" },
                values: new object[] { "-", "-" });

            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Alias", "CBU" },
                values: new object[] { "-", "-" });

            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Alias", "CBU" },
                values: new object[] { "MACRO.SECURE.SILO", "55948291235" });

            migrationBuilder.UpdateData(
                table: "FormasDePagos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Alias", "CBU" },
                values: new object[] { "MP.SECURE.SILO", "438850133263" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alias",
                table: "FormasDePagos");

            migrationBuilder.DropColumn(
                name: "CBU",
                table: "FormasDePagos");

            migrationBuilder.InsertData(
                table: "FormasDePagos",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 5, "Tarjeta de Credito" });
        }
    }
}
