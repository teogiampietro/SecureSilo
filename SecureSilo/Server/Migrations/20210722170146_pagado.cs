using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class pagado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pagado",
                table: "Suscripciones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pagado",
                table: "Suscripciones");
        }
    }
}
