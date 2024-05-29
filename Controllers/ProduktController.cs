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
    public class ProduktController : Controller
    {
        private readonly ProjektContext _context;

        public ProduktController(ProjektContext context)
        {
            _context = context;
        }

        // GET: Produkt
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CaloriesSortParm = sortOrder == "Calories" ? "calories_desc" : "Calories";
            ViewBag.CarbsSortParm = sortOrder == "Carbs" ? "carbs_desc" : "Carbs";
            ViewBag.FatsSortParm = sortOrder == "Fats" ? "fats_desc" : "Fats";
            ViewBag.ProteinsSortParm = sortOrder == "Proteins" ? "proteins_desc" : "Proteins";
            var produkty = from p in _context.Produkt
                           select p;

            switch (sortOrder)
            {
                case "name_desc":
                    produkty = produkty.OrderByDescending(p => p.Nazwa);
                    break;
                case "Calories":
                    produkty = produkty.OrderBy(p => p.Kalorycznosc);
                    break;
                case "calories_desc":
                    produkty = produkty.OrderByDescending(p => p.Kalorycznosc);
                    break;
                case "Carbs":
                    produkty = produkty.OrderBy(p => p.Weglowodany);
                    break;
                case "carbs_desc":
                    produkty = produkty.OrderByDescending(p => p.Weglowodany);
                    break;
                case "Fats":
                    produkty = produkty.OrderBy(p => p.Tluszcze);
                    break;
                case "fats_desc":
                    produkty = produkty.OrderByDescending(p => p.Tluszcze);
                    break;
                case "Proteins":
                    produkty = produkty.OrderBy(p => p.Bialka);
                    break;
                case "proteins_desc":
                    produkty = produkty.OrderByDescending(p => p.Bialka);
                    break;
                default:
                    produkty = produkty.OrderBy(p => p.Nazwa);
                    break;
            }

            return View(await produkty.ToListAsync());
        }

        // GET: Produkt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produkt == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt
                .FirstOrDefaultAsync(m => m.ProduktId == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // GET: Produkt/Create
        public IActionResult Create()
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Produkt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProduktId,Nazwa,Kalorycznosc,Weglowodany,Tluszcze,Bialka")] Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }

        // GET: Produkt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Produkt == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }
            return View(produkt);
        }

        // POST: Produkt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProduktId,Nazwa,Kalorycznosc,Weglowodany,Tluszcze,Bialka")] Produkt produkt)
        {
            if (id != produkt.ProduktId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produkt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktExists(produkt.ProduktId))
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
            return View(produkt);
        }

        // GET: Produkt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Produkt == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt
                .FirstOrDefaultAsync(m => m.ProduktId == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // POST: Produkt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produkt == null)
            {
                return Problem("Entity set 'ProjektContext.Produkt'  is null.");
            }
            var produkt = await _context.Produkt.FindAsync(id);
            if (produkt != null)
            {
                _context.Produkt.Remove(produkt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktExists(int id)
        {
          return (_context.Produkt?.Any(e => e.ProduktId == id)).GetValueOrDefault();
        }
    }
}
