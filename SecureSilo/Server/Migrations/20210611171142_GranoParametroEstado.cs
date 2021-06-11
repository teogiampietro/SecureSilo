using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class GranoParametroEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispositivos_Estados_EstadoId",
                table: "Dispositivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Parametros_Granos_GranoId",
                table: "Parametros");

            migrationBuilder.RenameColumn(
                name: "GranoId",
                table: "Parametros",
                newName: "GranoID");

            migrationBuilder.RenameIndex(
                name: "IX_Parametros_GranoId",
                table: "Parametros",
                newName: "IX_Parametros_GranoID");

            migrationBuilder.RenameColumn(
                name: "EstadoId",
                table: "Dispositivos",
                newName: "EstadoID");

            migrationBuilder.RenameIndex(
                name: "IX_Dispositivos_EstadoId",
                table: "Dispositivos",
                newName: "IX_Dispositivos_EstadoID");

            migrationBuilder.AlterColumn<int>(
                name: "GranoID",
                table: "Parametros",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EstadoID",
                table: "Dispositivos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Alerta" },
                    { 2, "SinDatos" },
                    { 3, "Ok" },
                    { 4, "Advertencia" }
                });

            migrationBuilder.InsertData(
                table: "Granos",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Soja" },
                    { 2, "Trigo" },
                    { 3, "Maiz" }
                });

            migrationBuilder.InsertData(
                table: "Parametros",
                columns: new[] { "Id", "CO2Value", "GranoID", "HumedadValue", "Riesgo", "TemperaturaValue" },
                values: new object[] { 1, 10.0, 1, 16.0, "Alto", 26.0 });

            migrationBuilder.InsertData(
                table: "Parametros",
                columns: new[] { "Id", "CO2Value", "GranoID", "HumedadValue", "Riesgo", "TemperaturaValue" },
                values: new object[] { 2, 10.0, 1, 14.0, "Medio", 24.0 });

            migrationBuilder.InsertData(
                table: "Parametros",
                columns: new[] { "Id", "CO2Value", "GranoID", "HumedadValue", "Riesgo", "TemperaturaValue" },
                values: new object[] { 3, 10.0, 1, 12.0, "Alto", 22.0 });

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositivos_Estados_EstadoID",
                table: "Dispositivos",
                column: "EstadoID",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parametros_Granos_GranoID",
                table: "Parametros",
                column: "GranoID",
                principalTable: "Granos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispositivos_Estados_EstadoID",
                table: "Dispositivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Parametros_Granos_GranoID",
                table: "Parametros");

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Granos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Granos",
                keyColumn: "Id",
                keyValue: 3);

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

            migrationBuilder.DeleteData(
                table: "Granos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "GranoID",
                table: "Parametros",
                newName: "GranoId");

            migrationBuilder.RenameIndex(
                name: "IX_Parametros_GranoID",
                table: "Parametros",
                newName: "IX_Parametros_GranoId");

            migrationBuilder.RenameColumn(
                name: "EstadoID",
                table: "Dispositivos",
                newName: "EstadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Dispositivos_EstadoID",
                table: "Dispositivos",
                newName: "IX_Dispositivos_EstadoId");

            migrationBuilder.AlterColumn<int>(
                name: "GranoId",
                table: "Parametros",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EstadoId",
                table: "Dispositivos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositivos_Estados_EstadoId",
                table: "Dispositivos",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parametros_Granos_GranoId",
                table: "Parametros",
                column: "GranoId",
                principalTable: "Granos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
