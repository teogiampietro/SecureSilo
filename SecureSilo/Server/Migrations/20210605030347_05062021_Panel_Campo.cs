using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class _05062021_Panel_Campo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Paneles_PanelID",
                table: "Silos");

            migrationBuilder.DropTable(
                name: "Paneles");

            migrationBuilder.DropIndex(
                name: "IX_Silos_PanelID",
                table: "Silos");

            migrationBuilder.DropColumn(
                name: "PanelID",
                table: "Silos");

            migrationBuilder.AddColumn<int>(
                name: "CampoID",
                table: "Silos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Campos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Silos_CampoID",
                table: "Silos",
                column: "CampoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Silos_Campos_CampoID",
                table: "Silos",
                column: "CampoID",
                principalTable: "Campos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Silos_Campos_CampoID",
                table: "Silos");

            migrationBuilder.DropTable(
                name: "Campos");

            migrationBuilder.DropIndex(
                name: "IX_Silos_CampoID",
                table: "Silos");

            migrationBuilder.DropColumn(
                name: "CampoID",
                table: "Silos");

            migrationBuilder.AddColumn<int>(
                name: "PanelID",
                table: "Silos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Paneles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paneles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Silos_PanelID",
                table: "Silos",
                column: "PanelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Silos_Paneles_PanelID",
                table: "Silos",
                column: "PanelID",
                principalTable: "Paneles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
