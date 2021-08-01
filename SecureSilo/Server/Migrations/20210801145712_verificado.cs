using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class verificado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Verificado",
                table: "Suscripciones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verificado",
                table: "Suscripciones");
        }
    }
}
