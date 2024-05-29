using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthenticationApp.Models;
using Projekt.Data;

namespace Projekt.Controllers
{
    public class DanieController : Controller
    {
        private readonly ProjektContext _context;

        public DanieController(ProjektContext context)
        {
            _context = context;
        }

        // GET: Danie
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.KalorycznoscSortParm = String.IsNullOrEmpty(sortOrder) ? "kalorycznosc_desc" : "";
            ViewBag.WeglowodanySortParm = sortOrder == "weglowodany" ? "weglowodany_desc" : "weglowodany";
            ViewBag.TluszczeSortParm = sortOrder == "tluszcze" ? "tluszcze_desc" : "tluszcze";
            ViewBag.BialkoSortParm = sortOrder == "bialko" ? "bialko_desc" : "bialko";

            var dania = await _context.Danie
                .Include(d => d.SkladDania)
                .ThenInclude(sd => sd.Produkt)
                .ToListAsync();

            var danieViewModels = dania.Select(d => new DanieViewModel
            {
                Danie = d,
                Kalorycznosc = d.SkladDania.Sum(sd => sd.Produkt.Kalorycznosc * sd.Ilosc / 100),
                Weglowodany = d.SkladDania.Sum(sd => sd.Produkt.Weglowodany * sd.Ilosc / 100),
                Tluszcze = d.SkladDania.Sum(sd => sd.Produkt.Tluszcze * sd.Ilosc / 100),
                Bialko = d.SkladDania.Sum(sd => sd.Produkt.Bialka * sd.Ilosc / 100)
            });

            switch (sortOrder)
            {
                case "kalorycznosc_desc":
                    danieViewModels = danieViewModels.OrderByDescending(d => d.Kalorycznosc);
                    break;
                case "weglowodany":
                    danieViewModels = danieViewModels.OrderBy(d => d.Weglowodany);
                    break;
                case "weglowodany_desc":
                    danieViewModels = danieViewModels.OrderByDescending(d => d.Weglowodany);
                    break;
                case "tluszcze":
                    danieViewModels = danieViewModels.OrderBy(d => d.Tluszcze);
                    break;
                case "tluszcze_desc":
                    danieViewModels = danieViewModels.OrderByDescending(d => d.Tluszcze);
                    break;
                case "bialko":
                    danieViewModels = danieViewModels.OrderBy(d => d.Bialko);
                    break;
                case "bialko_desc":
                    danieViewModels = danieViewModels.OrderByDescending(d => d.Bialko);
                    break;
                default:
                    danieViewModels = danieViewModels.OrderBy(d => d.Kalorycznosc);
                    break;
            }

            return View(danieViewModels);
        }

        // GET: Danie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Danie == null)
            {
                return NotFound();
            }

            var danie = await _context.Danie
                .Include(x=>x.SkladDania)
                .ThenInclude(sd=>sd.Produkt)
                .FirstOrDefaultAsync(m => m.DanieId == id);

            var skladDania = await _context.SkladDania
                .Include(x=>x.Danie)
                .Include(x=>x.Produkt)
                .Where(x => x.Danie.DanieId == id)
                .ToListAsync();
            if (danie == null)
            {
                return NotFound();
            }

            var danieViewModel = new DanieViewModel
            {
                Danie = danie,
                Kalorycznosc = skladDania.Sum(sd => sd.Produkt.Kalorycznosc * sd.Ilosc/100),
                Weglowodany = skladDania.Sum(sd => sd.Produkt.Weglowodany * sd.Ilosc/100),
                Tluszcze = skladDania.Sum(sd => sd.Produkt.Tluszcze * sd.Ilosc/100),
                Bialko = skladDania.Sum(sd => sd.Produkt.Bialka * sd.Ilosc/100)
            };

            return View(danieViewModel);
        }

        // GET: Danie/Create
        public IActionResult Create()
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Danie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DanieId,Nazwa")] Danie danie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danie);
        }

        // GET: Danie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            
            if (id == null || _context.Danie == null)
            {
                return NotFound();
            }

            var danie = await _context.Danie.FindAsync(id);
            if (danie == null)
            {
                return NotFound();
            }
            return View(danie);
        }

        // POST: Danie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DanieId,Nazwa")] Danie danie)
        {
            if (id != danie.DanieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanieExists(danie.DanieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(danie);
        }

        // GET: Danie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Danie == null)
            {
                return NotFound();
            }

            var danie = await _context.Danie
                .FirstOrDefaultAsync(m => m.DanieId == id);
            if (danie == null)
            {
                return NotFound();
            }

            return View(danie);
        }

        // POST: Danie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Danie == null)
            {
                return Problem("Entity set 'ProjektContext.Danie'  is null.");
            }
            var danie = await _context.Danie.FindAsync(id);
            if (danie != null)
            {
                _context.Danie.Remove(danie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanieExists(int id)
        {
          return (_context.Danie?.Any(e => e.DanieId == id)).GetValueOrDefault();
        }
    }
}
