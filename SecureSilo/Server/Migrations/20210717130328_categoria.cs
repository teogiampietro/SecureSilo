using Microsoft.EntityFrameworkCore.Migrations;

namespace SecureSilo.Server.Migrations
{
    public partial class categoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suscripciones_Categoria_CategoriaId",
                table: "Suscripciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria");

            migrationBuilder.RenameTable(
                name: "Categoria",
                newName: "Categorias");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Costo", "Descripción" },
                values: new object[] { 1, 2500.0, "Base" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Costo", "Descripción" },
                values: new object[] { 2, 5000.0, "Pro" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Costo", "Descripción" },
                values: new object[] { 3, 7000.0, "VIP" });

            migrationBuilder.AddForeignKey(
                name: "FK_Suscripciones_Categorias_CategoriaId",
                table: "Suscripciones",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suscripciones_Categorias_CategoriaId",
                table: "Suscripciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Categorias",
                newName: "Categoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suscripciones_Categoria_CategoriaId",
                table: "Suscripciones",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
