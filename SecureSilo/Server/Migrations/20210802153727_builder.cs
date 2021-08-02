using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class builder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 16.0, 30.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HumedadMaxValue", "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 16.0, 14.0, 30.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HumedadMaxValue", "TemperaturaValue" },
                values: new object[] { 14.0, 30.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 16.0, 30.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HumedadMaxValue", "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 16.0, 14.0, 30.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "HumedadMaxValue", "TemperaturaValue" },
                values: new object[] { 14.0, 30.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 16.0, 30.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "HumedadMaxValue", "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 16.0, 14.0, 30.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "HumedadMaxValue", "TemperaturaValue" },
                values: new object[] { 14.0, 30.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 14.0, 35.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HumedadMaxValue", "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 14.0, 10.0, 35.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HumedadMaxValue", "TemperaturaValue" },
                values: new object[] { 10.0, 35.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 14.0, 35.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HumedadMaxValue", "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 14.0, 10.0, 35.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "HumedadMaxValue", "TemperaturaValue" },
                values: new object[] { 10.0, 35.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 14.0, 35.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "HumedadMaxValue", "HumedadMinValue", "TemperaturaValue" },
                values: new object[] { 14.0, 10.0, 35.0 });

            migrationBuilder.UpdateData(
                table: "Parametros",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "HumedadMaxValue", "TemperaturaValue" },
                values: new object[] { 10.0, 35.0 });
        }
    }
}
