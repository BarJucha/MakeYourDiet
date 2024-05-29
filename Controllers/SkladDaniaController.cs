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
    public class SkladDaniaController : Controller
    {
        private readonly ProjektContext _context;

        public SkladDaniaController(ProjektContext context)
        {
            _context = context;
        }

        // GET: SkladDania
        public async Task<IActionResult> Index(int? id)
        {
            var skladDania = _context.SkladDania.Include(x => x.Danie).Include(x => x.Produkt).AsNoTracking().Where(x=>x.Danie.DanieId==id);
            var danie = _context.Danie.FirstOrDefault(x=>x.DanieId==id);
            ViewData["Danie"] = danie.Nazwa;
            ViewData["DanieId"] = danie.DanieId;
            return View(await skladDania.ToListAsync());
        }

        // GET: SkladDania/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SkladDania == null)
            {
                return NotFound();
            }

            var skladDania = await _context.SkladDania.Include(x=>x.Danie).Include(x=>x.Produkt)
                .FirstOrDefaultAsync(m => m.Danie.DanieId == id);
            if (skladDania == null)
            {
                return NotFound();
            }

            return View(skladDania);
        }

        private void PopulateDanieDropDownList(object selectedDanie = null)
        {
            var wybraneDanie = from e in _context.SkladDania orderby e.Danie select e;
            var res = wybraneDanie.AsNoTracking();
            ViewBag.DanieId = new SelectList(res, "Id", "Nazwa", selectedDanie);
        }

        private void PopulateProduktyDropDownList(object selectedProdukt = null)
        {
            var wybraneProdukty = from e in _context.Produkt
                                orderby e.Nazwa
                                select e;
            var res = wybraneProdukty.AsNoTracking();
            ViewBag.ZespolyID = new SelectList(res, "Id_produktu", "Produkt", selectedProdukt);
        }

        // GET: SkladDania/Create
        public IActionResult Create(int? danieId)
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            PopulateDanieDropDownList(danieId);
            PopulateProduktyDropDownList();
            var produktyLista = _context.Produkt.ToList(); // Pobierz wszystkie produkty
            var daniaLista = _context.Danie.ToList();
            var podstawoweDanie = daniaLista.Where(d => d.DanieId==danieId).FirstOrDefault();
            ViewData["ProduktyLista"] = produktyLista;
            ViewData["DaniaLista"] = daniaLista;
            ViewData["DaniePodstawoweId"] = podstawoweDanie.DanieId;
            ViewData["DaniePodstawoweName"] = podstawoweDanie.Nazwa;
            return View();
        }

        // POST: SkladDania/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SkladDaniaId,Ilosc")] SkladDania skladDania, IFormCollection form)
        {
            string danieValue = form["Danie"].ToString();
            System.Console.WriteLine(danieValue);
            string produktValue = form["Produkt"].ToString();
            if (!ModelState.IsValid)
            {
                Danie danie = null;
                if (danieValue != "-1")
                {
                    var ee = _context.Danie.Where(d => d.DanieId == int.Parse(danieValue));
                    if (ee.Count() > 0) danie = ee.First();
                }
                Produkt produkt = null;
                if (produktValue != "-1")
                {
                    var ee = _context.Produkt.Where(d => d.ProduktId == int.Parse(produktValue));
                    if (ee.Count() > 0) produkt = ee.First();
                }
                skladDania.Produkt = produkt;
                skladDania.Danie = danie;
                _context.Add(skladDania);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {id=danie.DanieId});
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: SkladDania/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.SkladDania == null)
            {
                return NotFound();
            }

            var skladDania = _context.SkladDania.Where(p=>p.SkladDaniaId==id).Include(p=>p.Danie).Include(p=>p.Produkt).First();
            if (skladDania == null)
            {
                return NotFound();
            }
            if (skladDania.Produkt != null)
            {
                PopulateProduktyDropDownList(skladDania.Produkt.ProduktId);
            }
            else
            {
                PopulateProduktyDropDownList();
            }
            if (skladDania.Danie != null)
            {
                PopulateDanieDropDownList(skladDania.Danie.DanieId);
            }
            else
            {
                PopulateDanieDropDownList();
            }
            return View(skladDania);
        }

        // POST: SkladDania/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SkladDaniaId,Ilosc")] SkladDania skladDania, IFormCollection form)
        {
            if (id != skladDania.SkladDaniaId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    //_context.Update(skladDania);
                    SkladDania sd = _context.SkladDania.Where(p=>p.SkladDaniaId == id).Include(p=>p.Danie).Include(p=>p.Produkt).First();
                    sd.Ilosc = skladDania.Ilosc;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new{id=sd.Danie.DanieId});
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkladDaniaExists(skladDania.SkladDaniaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(skladDania);
        }

        // GET: SkladDania/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var ifAdmin = HttpContext.Session.GetString("Admin");
            if(string.IsNullOrEmpty(ifAdmin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.SkladDania == null)
            {
                return NotFound();
            }

            var skladDania = _context.SkladDania.Where(p=>p.SkladDaniaId==id).Include(p=>p.Danie).Include(p=>p.Produkt).First();
            if (skladDania == null)
            {
                return NotFound();
            }
            ViewData["Składnik"] = skladDania.Produkt.Nazwa;
            return View(skladDania);
        }

        // POST: SkladDania/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int danieId)
        {
            if (_context.SkladDania == null)
            {
                return Problem("Entity set 'ProjektContext.SkladDania'  is null.");
            }
            var skladDania = await _context.SkladDania.FindAsync(id);
            if (skladDania != null)
            {
                _context.SkladDania.Remove(skladDania);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {id=danieId});
        }

        private bool SkladDaniaExists(int id)
        {
          return (_context.SkladDania?.Any(e => e.SkladDaniaId == id)).GetValueOrDefault();
        }
    }
}
