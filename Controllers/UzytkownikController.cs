using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthenticationApp.Models;
using Projekt.Data;
using Microsoft.AspNetCore.Authorization;

namespace Projekt.Controllers
{
    public class UzytkownikController : Controller
    {
        private readonly ProjektContext _context;

        public UzytkownikController(ProjektContext context)
        {
            _context = context;
        }

        // GET: Uzytkownik
        public async Task<IActionResult> Index()
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
              return _context.Uzytkownik != null ? 
                          View(await _context.Uzytkownik.ToListAsync()) :
                          Problem("Entity set 'ProjektContext.Uzytkownik'  is null.");
        }

        // GET: Uzytkownik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Uzytkownik == null)
            {
                return NotFound();
            }

            var uzytkownik = await _context.Uzytkownik
                .FirstOrDefaultAsync(m => m.UzytkownikId == id);
            if (uzytkownik == null)
            {
                return NotFound();
            }

            return View(uzytkownik);
        }

        // GET: Uzytkownik/Create
        public IActionResult Create()
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Uzytkownik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UzytkownikId,Login,Haslo,CzyAdmin")] Uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                uzytkownik.SetHashedPassword(uzytkownik.Haslo);
                _context.Add(uzytkownik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uzytkownik);
        }

        // GET: Uzytkownik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Uzytkownik == null)
            {
                return NotFound();
            }

            var uzytkownik = await _context.Uzytkownik.FindAsync(id);
            if (uzytkownik == null)
            {
                return NotFound();
            }
            return View(uzytkownik);
        }

        // POST: Uzytkownik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UzytkownikId,Login,Haslo,CzyAdmin")] Uzytkownik uzytkownik)
        {
            if (id != uzytkownik.UzytkownikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uzytkownik.SetHashedPassword(uzytkownik.Haslo);
                    _context.Update(uzytkownik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UzytkownikExists(uzytkownik.UzytkownikId))
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
            return View(uzytkownik);
        }

        // GET: Uzytkownik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Uzytkownik == null)
            {
                return NotFound();
            }

            var uzytkownik = await _context.Uzytkownik
                .FirstOrDefaultAsync(m => m.UzytkownikId == id);
            if (uzytkownik == null)
            {
                return NotFound();
            }

            return View(uzytkownik);
        }

        // POST: Uzytkownik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Uzytkownik == null)
            {
                return Problem("Entity set 'ProjektContext.Uzytkownik'  is null.");
            }
            var uzytkownik = await _context.Uzytkownik.FindAsync(id);
            if (uzytkownik != null)
            {
                _context.Uzytkownik.Remove(uzytkownik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UzytkownikExists(int id)
        {
          return (_context.Uzytkownik?.Any(e => e.UzytkownikId == id)).GetValueOrDefault();
        }
    }
}
