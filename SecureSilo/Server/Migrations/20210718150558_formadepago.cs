using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class formadepago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormaDePago",
                table: "Suscripciones");

            migrationBuilder.AddColumn<int>(
                name: "FormaDePagoId",
                table: "Suscripciones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FormasDePagos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasDePagos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FormasDePagos",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Efectivo" },
                    { 2, "Transferencia" },
                    { 3, "Mercado Pago" },
                    { 4, "Tarjeta Credito" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_FormaDePagoId",
                table: "Suscripciones",
                column: "FormaDePagoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suscripciones_FormasDePagos_FormaDePagoId",
                table: "Suscripciones",
                column: "FormaDePagoId",
                principalTable: "FormasDePagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suscripciones_FormasDePagos_FormaDePagoId",
                table: "Suscripciones");

            migrationBuilder.DropTable(
                name: "FormasDePagos");

            migrationBuilder.DropIndex(
                name: "IX_Suscripciones_FormaDePagoId",
                table: "Suscripciones");

            migrationBuilder.DropColumn(
                name: "FormaDePagoId",
                table: "Suscripciones");

            migrationBuilder.AddColumn<string>(
                name: "FormaDePago",
                table: "Suscripciones",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
