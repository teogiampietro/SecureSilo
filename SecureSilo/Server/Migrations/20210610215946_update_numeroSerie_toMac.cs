using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class update_numeroSerie_toMac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroSerie",
                table: "Updates");

            migrationBuilder.AddColumn<string>(
                name: "MAC",
                table: "Updates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MAC",
                table: "Updates");

            migrationBuilder.AddColumn<string>(
                name: "NumeroSerie",
                table: "Updates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
