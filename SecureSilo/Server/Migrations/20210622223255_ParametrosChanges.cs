using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class ParametrosChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CO2Value",
                table: "Parametros");

            migrationBuilder.DropColumn(
                name: "HumedadValue",
                table: "Parametros");

            migrationBuilder.AddColumn<double>(
                name: "HumedadMaxValue",
                table: "Parametros",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HumedadMinValue",
                table: "Parametros",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1,
                column: "Riesgo",
                value: "SIN_RIESGO");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 5,
                column: "Riesgo",
                value: "SIN_RIESGO");

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HumedadMaxValue", "HumedadMinValue" },
                values: new object[] { 99.0, 14.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HumedadMaxValue", "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 14.0, 10.0, 26.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HumedadMaxValue", "TemperaturaValue" },
                values: new object[] { 10.0, 26.0 });

            migrationBuilder.InsertData(
                table: "Parametros",
                columns: new[] { "Id", "GranoID", "HumedadMaxValue", "HumedadMinValue", "Riesgo", "TemperaturaValue" },
                values: new object[,]
                {
                    { 4, 2, 99.0, 14.0, "ALTO", 26.0 },
                    { 5, 2, 14.0, 10.0, "MEDIO", 26.0 },
                    { 6, 2, 10.0, 0.0, "BAJO", 26.0 },
                    { 7, 3, 99.0, 14.0, "ALTO", 26.0 },
                    { 8, 3, 14.0, 10.0, "MEDIO", 26.0 },
                    { 9, 3, 10.0, 0.0, "BAJO", 26.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "HumedadMaxValue",
                table: "Parametros");

            migrationBuilder.DropColumn(
                name: "HumedadMinValue",
                table: "Parametros");

            migrationBuilder.AddColumn<double>(
                name: "CO2Value",
                table: "Parametros",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HumedadValue",
                table: "Parametros",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1,
                column: "Riesgo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 5,
                column: "Riesgo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CO2Value", "HumedadValue" },
                values: new object[] { 10.0, 16.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CO2Value", "HumedadValue", "TemperaturaValue" },
                values: new object[] { 10.0, 14.0, 24.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CO2Value", "HumedadValue", "TemperaturaValue" },
                values: new object[] { 10.0, 12.0, 22.0 });
        }
    }
}
