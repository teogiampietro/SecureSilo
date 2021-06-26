using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class intnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Campos_CampoID",
                table: "Silos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Granos_GranoID",
                table: "Silos");

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

            migrationBuilder.AlterColumn<int>(
                name: "GranoID",
                table: "Silos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CampoID",
                table: "Silos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 5, "ADVERTENCIA" });

            migrationBuilder.InsertData(
                table: "Parametros",
                columns: new[] { "Id", "CO2Value", "GranoID", "HumedadValue", "Riesgo", "TemperaturaValue" },
                values: new object[,]
                {
                    { 4, 10.0, 1, 16.0, "ALTO", 26.0 },
                    { 5, 10.0, 1, 14.0, "MEDIO", 24.0 },
                    { 6, 10.0, 1, 12.0, "BAJO", 22.0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Silos_Campos_CampoID",
                table: "Silos",
                column: "CampoID",
                principalTable: "Campos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Silos_Granos_GranoID",
                table: "Silos",
                column: "GranoID",
                principalTable: "Granos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Campos_CampoID",
                table: "Silos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Granos_GranoID",
                table: "Silos");

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 5);

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

            migrationBuilder.AlterColumn<int>(
                name: "GranoID",
                table: "Silos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CampoID",
                table: "Silos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descripcion",
                value: "Alerta");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descripcion",
                value: "SinEstado");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 3,
                column: "Descripcion",
                value: "Ok");

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 4,
                column: "Descripcion",
                value: "Advertencia");

            migrationBuilder.InsertData(
                table: "Parametros",
                columns: new[] { "Id", "CO2Value", "GranoID", "HumedadValue", "Riesgo", "TemperaturaValue" },
                values: new object[,]
                {
                    { 1, 10.0, 1, 16.0, "Alto", 26.0 },
                    { 2, 10.0, 1, 14.0, "Medio", 24.0 },
                    { 3, 10.0, 1, 12.0, "Bajo", 22.0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Silos_Campos_CampoID",
                table: "Silos",
                column: "CampoID",
                principalTable: "Campos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Silos_Granos_GranoID",
                table: "Silos",
                column: "GranoID",
                principalTable: "Granos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
