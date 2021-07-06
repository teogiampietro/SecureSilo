using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class userIdsilodsp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Silos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Dispositivos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Silos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Dispositivos");
        }
    }
}
