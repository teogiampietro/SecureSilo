using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class estado_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Silos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Dispositivos");

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Silos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Dispositivos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Silos_EstadoId",
                table: "Silos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivos_EstadoId",
                table: "Dispositivos",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositivos_Estados_EstadoId",
                table: "Dispositivos",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Silos_Estados_EstadoId",
                table: "Silos",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispositivos_Estados_EstadoId",
                table: "Dispositivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Estados_EstadoId",
                table: "Silos");

            migrationBuilder.DropIndex(
                name: "IX_Silos_EstadoId",
                table: "Silos");

            migrationBuilder.DropIndex(
                name: "IX_Dispositivos_EstadoId",
                table: "Dispositivos");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Silos");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Dispositivos");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Silos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Dispositivos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
