using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class suscripcionUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Suscripciones",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_ApplicationUserId",
                table: "Suscripciones",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suscripciones_AspNetUsers_ApplicationUserId",
                table: "Suscripciones",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suscripciones_AspNetUsers_ApplicationUserId",
                table: "Suscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Suscripciones_ApplicationUserId",
                table: "Suscripciones");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Suscripciones");
        }
    }
}
