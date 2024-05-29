using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Danie",
                columns: table => new
                {
                    DanieId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Danie", x => x.DanieId);
                });

            migrationBuilder.CreateTable(
                name: "Produkt",
                columns: table => new
                {
                    ProduktId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Kalorycznosc = table.Column<int>(type: "INTEGER", nullable: false),
                    Weglowodany = table.Column<int>(type: "INTEGER", nullable: false),
                    Tluszcze = table.Column<int>(type: "INTEGER", nullable: false),
                    Bialka = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkt", x => x.ProduktId);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownik",
                columns: table => new
                {
                    UzytkownikId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Haslo = table.Column<string>(type: "TEXT", nullable: false),
                    CzyAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownik", x => x.UzytkownikId);
                });

            migrationBuilder.CreateTable(
                name: "SkladDania",
                columns: table => new
                {
                    SkladDaniaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ilosc = table.Column<int>(type: "INTEGER", nullable: false),
                    DanieId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduktId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkladDania", x => x.SkladDaniaId);
                    table.ForeignKey(
                        name: "FK_SkladDania_Danie_DanieId",
                        column: x => x.DanieId,
                        principalTable: "Danie",
                        principalColumn: "DanieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkladDania_Produkt_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkt",
                        principalColumn: "ProduktId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanDnia",
                columns: table => new
                {
                    PlanDniaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataDnia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UzytkownikId = table.Column<int>(type: "INTEGER", nullable: false),
                    DanieId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanDnia", x => x.PlanDniaId);
                    table.ForeignKey(
                        name: "FK_PlanDnia_Danie_DanieId",
                        column: x => x.DanieId,
                        principalTable: "Danie",
                        principalColumn: "DanieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanDnia_Uzytkownik_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownik",
                        principalColumn: "UzytkownikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanDnia_DanieId",
                table: "PlanDnia",
                column: "DanieId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanDnia_UzytkownikId",
                table: "PlanDnia",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_SkladDania_DanieId",
                table: "SkladDania",
                column: "DanieId");

            migrationBuilder.CreateIndex(
                name: "IX_SkladDania_ProduktId",
                table: "SkladDania",
                column: "ProduktId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanDnia");

            migrationBuilder.DropTable(
                name: "SkladDania");

            migrationBuilder.DropTable(
                name: "Uzytkownik");

            migrationBuilder.DropTable(
                name: "Danie");

            migrationBuilder.DropTable(
                name: "Produkt");
        }
    }
}
