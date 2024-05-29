using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class DodanieProduktow5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Uzytkownik",
                keyColumn: "UzytkownikId",
                keyValue: 10,
                column: "Haslo",
                value: "202cb962ac59075b964b07152d234b70");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Uzytkownik",
                keyColumn: "UzytkownikId",
                keyValue: 10,
                column: "Haslo",
                value: "admin123");
        }
    }
}
