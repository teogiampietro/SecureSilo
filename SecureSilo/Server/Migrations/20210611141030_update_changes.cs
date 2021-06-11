using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class update_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CO2",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "FechaHora",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "Humedad",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "MAC",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "Movimiento",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "Temperatura",
                table: "Updates");

            migrationBuilder.AddColumn<string>(
                name: "A",
                table: "Updates",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "C",
                table: "Updates",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "F",
                table: "Updates",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "H",
                table: "Updates",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "M",
                table: "Updates",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "T",
                table: "Updates",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "A",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "C",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "F",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "H",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "M",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "T",
                table: "Updates");

            migrationBuilder.AddColumn<double>(
                name: "CO2",
                table: "Updates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "FechaHora",
                table: "Updates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Humedad",
                table: "Updates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MAC",
                table: "Updates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Movimiento",
                table: "Updates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Temperatura",
                table: "Updates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
