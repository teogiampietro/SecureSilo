using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class riesgo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AddColumn<string>(
                name: "Riesgo",
                table: "Estados",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descripcion",
                value: "DEFAULT");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descripcion",
                value: "OK");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 3,
                column: "Descripcion",
                value: "ADVERTENCIA");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 4,
                column: "Descripcion",
                value: "ALERTA");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 5,
                column: "Descripcion",
                value: "SIN_DATOS");

            migrationBuilder.InsertData(
                table: "Parametros",
                columns: new[] { "Id", "CO2Value", "GranoID", "HumedadValue", "Riesgo", "TemperaturaValue" },
                values: new object[,]
                {
                    { 1, 10.0, 1, 16.0, "ALTO", 26.0 },
                    { 2, 10.0, 1, 14.0, "MEDIO", 24.0 },
                    { 3, 10.0, 1, 12.0, "BAJO", 22.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Riesgo",
                table: "Estados");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descripcion",
                value: "Default");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descripcion",
                value: "ALERTA");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 3,
                column: "Descripcion",
                value: "SIN_DATOS");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 4,
                column: "Descripcion",
                value: "OK");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 5,
                column: "Descripcion",
                value: "ADVERTENCIA");

            migrationBuilder.InsertData(
                table: "Parametros",
                columns: new[] { "Id", "CO2Value", "GranoID", "HumedadValue", "Riesgo", "TemperaturaValue" },
                values: new object[,]
                {
                    { 4, 10.0, 1, 16.0, "ALTO", 26.0 },
                    { 5, 10.0, 1, 14.0, "MEDIO", 24.0 },
                    { 6, 10.0, 1, 12.0, "BAJO", 22.0 }
                });
        }
    }
}
