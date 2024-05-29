using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthenticationApp.Models;
using Projekt.Controllers;

namespace Projekt.Data
{
    public class ProjektContext : DbContext
    {
        public ProjektContext (DbContextOptions<ProjektContext> options)
            : base(options)
        {
        }

        public DbSet<AuthenticationApp.Models.Uzytkownik> Uzytkownik { get; set; } = default!;

        public DbSet<AuthenticationApp.Models.Produkt> Produkt { get; set; } = default!;

        public DbSet<AuthenticationApp.Models.Danie> Danie { get; set; } = default!;

        public DbSet<AuthenticationApp.Models.PlanDnia> PlanDnia { get; set; } = default!;

        public DbSet<AuthenticationApp.Models.SkladDania> SkladDania { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            string hashedPass = HomeController.SetHashedPassword("123");
            modelBuilder.Entity<Uzytkownik>().HasData(
                new Uzytkownik { UzytkownikId = 10, Login = "admin", Haslo = hashedPass, CzyAdmin = true }
            );

            modelBuilder.Entity<Produkt>().HasData(
                new Produkt { ProduktId = 1, Nazwa = "Jabłko", Kalorycznosc = 52, Weglowodany = 14, Tluszcze = 0, Bialka = 0 },
                new Produkt { ProduktId = 2, Nazwa = "Banan", Kalorycznosc = 89, Weglowodany = 23, Tluszcze = 0, Bialka = 1 },
                new Produkt { ProduktId = 3, Nazwa = "Marchewka", Kalorycznosc = 41, Weglowodany = 10, Tluszcze = 0, Bialka = 1 },
                new Produkt { ProduktId = 4, Nazwa = "Pierś z kurczaka", Kalorycznosc = 165, Weglowodany = 0, Tluszcze = 3, Bialka = 31 },
                new Produkt { ProduktId = 5, Nazwa = "Brokuł", Kalorycznosc = 55, Weglowodany = 11, Tluszcze = 0, Bialka = 4 },
                new Produkt { ProduktId = 6, Nazwa = "Ryż", Kalorycznosc = 130, Weglowodany = 28, Tluszcze = 0, Bialka = 3 },
                new Produkt { ProduktId = 7, Nazwa = "Łosoś", Kalorycznosc = 208, Weglowodany = 0, Tluszcze = 13, Bialka = 20 },
                new Produkt { ProduktId = 8, Nazwa = "Jajko", Kalorycznosc = 78, Weglowodany = 1, Tluszcze = 5, Bialka = 6 },
                new Produkt { ProduktId = 9, Nazwa = "Mleko", Kalorycznosc = 42, Weglowodany = 5, Tluszcze = 1, Bialka = 3 },
                new Produkt { ProduktId = 10, Nazwa = "Migdały", Kalorycznosc = 576, Weglowodany = 22, Tluszcze = 49, Bialka = 21 }
            );

            modelBuilder.Entity<Danie>().HasData(
                new Danie { DanieId = 1, Nazwa = "Sałatka owocowa" },
                new Danie { DanieId = 2, Nazwa = "Ryż z kurczakiem" },
                new Danie { DanieId = 3, Nazwa = "Podsmażane brokuły" }
            );
        }
    }
}
