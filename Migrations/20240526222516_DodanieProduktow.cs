using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class DodanieProduktow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Uzytkownik",
                keyColumn: "UzytkownikId",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Uzytkownik",
                columns: new[] { "UzytkownikId", "CzyAdmin", "Haslo", "Login" },
                values: new object[] { 1, true, "admin123", "admin" });
        }
    }
}
