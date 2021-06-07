using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class _06062021_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Campos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Campos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Campos_ApplicationUserId",
                table: "Campos",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campos_AspNetUsers_ApplicationUserId",
                table: "Campos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campos_AspNetUsers_ApplicationUserId",
                table: "Campos");

            migrationBuilder.DropIndex(
                name: "IX_Campos_ApplicationUserId",
                table: "Campos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Campos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Campos");
        }
    }
}
