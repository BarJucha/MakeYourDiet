using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class DodaniePrzykladow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Danie",
                columns: new[] { "DanieId", "Nazwa" },
                values: new object[,]
                {
                    { 1, "Sałatka owocowa" },
                    { 2, "Ryż z kurczakiem" },
                    { 3, "Podsmażane brokuły" }
                });

            migrationBuilder.InsertData(
                table: "Produkt",
                columns: new[] { "ProduktId", "Bialka", "Kalorycznosc", "Nazwa", "Tluszcze", "Weglowodany" },
                values: new object[,]
                {
                    { 1, 0, 52, "Jabłko", 0, 14 },
                    { 2, 1, 89, "Banan", 0, 23 },
                    { 3, 1, 41, "Marchewka", 0, 10 },
                    { 4, 31, 165, "Pierś z kurczaka", 3, 0 },
                    { 5, 4, 55, "Brokuł", 0, 11 },
                    { 6, 3, 130, "Ryż", 0, 28 },
                    { 7, 20, 208, "Łosoś", 13, 0 },
                    { 8, 6, 78, "Jajko", 5, 1 },
                    { 9, 3, 42, "Mleko", 1, 5 },
                    { 10, 21, 576, "Migdały", 49, 22 }
                });

            migrationBuilder.InsertData(
                table: "Uzytkownik",
                columns: new[] { "UzytkownikId", "CzyAdmin", "Haslo", "Login" },
                values: new object[] { 1, true, "admin123", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Danie",
                keyColumn: "DanieId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Danie",
                keyColumn: "DanieId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Danie",
                keyColumn: "DanieId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Produkt",
                keyColumn: "ProduktId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Produkt",
                keyColumn: "ProduktId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produkt",
                keyColumn: "ProduktId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Produkt",
                keyColumn: "ProduktId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Produkt",
                keyColumn: "ProduktId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Produkt",
                keyColumn: "ProduktId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Produkt",
                keyColumn: "ProduktId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Produkt",
                keyColumn: "ProduktId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Produkt",
                keyColumn: "ProduktId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Produkt",
                keyColumn: "ProduktId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Uzytkownik",
                keyColumn: "UzytkownikId",
                keyValue: 1);
        }
    }
}
