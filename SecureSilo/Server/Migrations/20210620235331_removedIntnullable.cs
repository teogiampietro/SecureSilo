using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class removedIntnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Campos_CampoID",
                table: "Silos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Estados_EstadoId",
                table: "Silos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Granos_GranoID",
                table: "Silos");

            migrationBuilder.AlterColumn<int>(
                name: "GranoID",
                table: "Silos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EstadoId",
                table: "Silos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CampoID",
                table: "Silos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Silos_Campos_CampoID",
                table: "Silos",
                column: "CampoID",
                principalTable: "Campos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Silos_Estados_EstadoId",
                table: "Silos",
                column: "EstadoId",
                principalTable: "Estados",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Campos_CampoID",
                table: "Silos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Estados_EstadoId",
                table: "Silos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Granos_GranoID",
                table: "Silos");

            migrationBuilder.AlterColumn<int>(
                name: "GranoID",
                table: "Silos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EstadoId",
                table: "Silos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CampoID",
                table: "Silos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Silos_Campos_CampoID",
                table: "Silos",
                column: "CampoID",
                principalTable: "Campos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Silos_Estados_EstadoId",
                table: "Silos",
                column: "EstadoId",
                principalTable: "Estados",
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
    }
}
