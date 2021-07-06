using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class actualizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Updates");

            migrationBuilder.CreateTable(
                name: "Actualizaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    M = table.Column<string>(nullable: true),
                    F = table.Column<string>(nullable: true),
                    A = table.Column<string>(nullable: true),
                    T = table.Column<double>(nullable: false),
                    H = table.Column<double>(nullable: false),
                    C = table.Column<double>(nullable: false),
                    DispositivoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actualizaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actualizaciones_Dispositivos_DispositivoID",
                        column: x => x.DispositivoID,
                        principalTable: "Dispositivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actualizaciones_DispositivoID",
                table: "Actualizaciones",
                column: "DispositivoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actualizaciones");

            migrationBuilder.CreateTable(
                name: "Updates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    A = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C = table.Column<double>(type: "float", nullable: false),
                    DispositivoID = table.Column<int>(type: "int", nullable: false),
                    F = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H = table.Column<double>(type: "float", nullable: false),
                    M = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    T = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Updates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Updates_Dispositivos_DispositivoID",
                        column: x => x.DispositivoID,
                        principalTable: "Dispositivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Updates_DispositivoID",
                table: "Updates",
                column: "DispositivoID");
        }
    }
}
