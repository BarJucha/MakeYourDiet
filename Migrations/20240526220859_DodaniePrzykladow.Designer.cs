﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projekt.Data;

#nullable disable

namespace Projekt.Migrations
{
    [DbContext(typeof(ProjektContext))]
    [Migration("20240526220859_DodaniePrzykladow")]
    partial class DodaniePrzykladow
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("AuthenticationApp.Models.Danie", b =>
                {
                    b.Property<int>("DanieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DanieId");

                    b.ToTable("Danie");

                    b.HasData(
                        new
                        {
                            DanieId = 1,
                            Nazwa = "Sałatka owocowa"
                        },
                        new
                        {
                            DanieId = 2,
                            Nazwa = "Ryż z kurczakiem"
                        },
                        new
                        {
                            DanieId = 3,
                            Nazwa = "Podsmażane brokuły"
                        });
                });

            modelBuilder.Entity("AuthenticationApp.Models.PlanDnia", b =>
                {
                    b.Property<int>("PlanDniaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DanieId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataDnia")
                        .HasColumnType("TEXT");

                    b.Property<int>("UzytkownikId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlanDniaId");

                    b.HasIndex("DanieId");

                    b.HasIndex("UzytkownikId");

                    b.ToTable("PlanDnia");
                });

            modelBuilder.Entity("AuthenticationApp.Models.Produkt", b =>
                {
                    b.Property<int>("ProduktId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Bialka")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Kalorycznosc")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Tluszcze")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Weglowodany")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProduktId");

                    b.ToTable("Produkt");

                    b.HasData(
                        new
                        {
                            ProduktId = 1,
                            Bialka = 0,
                            Kalorycznosc = 52,
                            Nazwa = "Jabłko",
                            Tluszcze = 0,
                            Weglowodany = 14
                        },
                        new
                        {
                            ProduktId = 2,
                            Bialka = 1,
                            Kalorycznosc = 89,
                            Nazwa = "Banan",
                            Tluszcze = 0,
                            Weglowodany = 23
                        },
                        new
                        {
                            ProduktId = 3,
                            Bialka = 1,
                            Kalorycznosc = 41,
                            Nazwa = "Marchewka",
                            Tluszcze = 0,
                            Weglowodany = 10
                        },
                        new
                        {
                            ProduktId = 4,
                            Bialka = 31,
                            Kalorycznosc = 165,
                            Nazwa = "Pierś z kurczaka",
                            Tluszcze = 3,
                            Weglowodany = 0
                        },
                        new
                        {
                            ProduktId = 5,
                            Bialka = 4,
                            Kalorycznosc = 55,
                            Nazwa = "Brokuł",
                            Tluszcze = 0,
                            Weglowodany = 11
                        },
                        new
                        {
                            ProduktId = 6,
                            Bialka = 3,
                            Kalorycznosc = 130,
                            Nazwa = "Ryż",
                            Tluszcze = 0,
                            Weglowodany = 28
                        },
                        new
                        {
                            ProduktId = 7,
                            Bialka = 20,
                            Kalorycznosc = 208,
                            Nazwa = "Łosoś",
                            Tluszcze = 13,
                            Weglowodany = 0
                        },
                        new
                        {
                            ProduktId = 8,
                            Bialka = 6,
                            Kalorycznosc = 78,
                            Nazwa = "Jajko",
                            Tluszcze = 5,
                            Weglowodany = 1
                        },
                        new
                        {
                            ProduktId = 9,
                            Bialka = 3,
                            Kalorycznosc = 42,
                            Nazwa = "Mleko",
                            Tluszcze = 1,
                            Weglowodany = 5
                        },
                        new
                        {
                            ProduktId = 10,
                            Bialka = 21,
                            Kalorycznosc = 576,
                            Nazwa = "Migdały",
                            Tluszcze = 49,
                            Weglowodany = 22
                        });
                });

            modelBuilder.Entity("AuthenticationApp.Models.SkladDania", b =>
                {
                    b.Property<int>("SkladDaniaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DanieId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ilosc")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProduktId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SkladDaniaId");

                    b.HasIndex("DanieId");

                    b.HasIndex("ProduktId");

                    b.ToTable("SkladDania");
                });

            modelBuilder.Entity("AuthenticationApp.Models.Uzytkownik", b =>
                {
                    b.Property<int>("UzytkownikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CzyAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Haslo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UzytkownikId");

                    b.ToTable("Uzytkownik");

                    b.HasData(
                        new
                        {
                            UzytkownikId = 1,
                            CzyAdmin = true,
                            Haslo = "admin123",
                            Login = "admin"
                        });
                });

            modelBuilder.Entity("AuthenticationApp.Models.PlanDnia", b =>
                {
                    b.HasOne("AuthenticationApp.Models.Danie", "Danie")
                        .WithMany("PlanDnia")
                        .HasForeignKey("DanieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthenticationApp.Models.Uzytkownik", "Uzytkownik")
                        .WithMany("PlanDnia")
                        .HasForeignKey("UzytkownikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Danie");

                    b.Navigation("Uzytkownik");
                });

            modelBuilder.Entity("AuthenticationApp.Models.SkladDania", b =>
                {
                    b.HasOne("AuthenticationApp.Models.Danie", "Danie")
                        .WithMany("SkladDania")
                        .HasForeignKey("DanieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthenticationApp.Models.Produkt", "Produkt")
                        .WithMany("SkladDania")
                        .HasForeignKey("ProduktId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Danie");

                    b.Navigation("Produkt");
                });

            modelBuilder.Entity("AuthenticationApp.Models.Danie", b =>
                {
                    b.Navigation("PlanDnia");

                    b.Navigation("SkladDania");
                });

            modelBuilder.Entity("AuthenticationApp.Models.Produkt", b =>
                {
                    b.Navigation("SkladDania");
                });

            modelBuilder.Entity("AuthenticationApp.Models.Uzytkownik", b =>
                {
                    b.Navigation("PlanDnia");
                });
#pragma warning restore 612, 618
        }
    }
}
