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
    public class PlanDniaController : Controller
    {
        private readonly ProjektContext _context;

        public PlanDniaController(ProjektContext context)
        {
            _context = context;
        }

        // GET: PlanDnia
        public async Task<IActionResult> Index()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");

            if (userID == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var plan = _context.PlanDnia.Include(p=>p.Uzytkownik).Include(p=>p.Danie).Where(p=>p.Uzytkownik.UzytkownikId==userID).AsNoTracking();

            var groupedPlaned = plan
                .GroupBy(p => p.DataDnia.Date)
                .OrderBy(g=>g.Key)
                .Select(g=>new PlanDniaGroupViewModel
                {
                    Date = g.Key,
                    Plany = g.OrderBy(p=>p.DataDnia).ToList()
                }).ToList();

            ViewData["GroupedPlanyDnia"] = groupedPlaned;

            return View(groupedPlaned);
        }

        // GET: PlanDnia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var username = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.PlanDnia == null)
            {
                return NotFound();
            }

            var planDnia = await _context.PlanDnia
                .Include(p=>p.Danie)
                .Include(p=>p.Uzytkownik)
                .FirstOrDefaultAsync(m => m.PlanDniaId == id);
            if (planDnia == null)
            {
                return NotFound();
            }

            return View(planDnia);
        }

        private void PopulateDaniaDropDownList(object selectedDanie = null)
        {
            var wybraneDanie = from d in _context.Danie orderby d.Nazwa select d;
            var res = wybraneDanie.AsNoTracking();
            ViewBag.DanieID = new SelectList(res, "Id_dania", "Nazwa", selectedDanie);
        }

        // GET: PlanDnia/Create
        public IActionResult Create()
        {
            var username = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }
            var userID = HttpContext.Session.GetInt32("UserID");
            PopulateDaniaDropDownList();
            ViewData["UserID"] = userID;
            ViewData["ListaDan"] = _context.Danie.ToList();
            return View();
        }

        // POST: PlanDnia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanDniaId,DataDnia")] PlanDnia planDnia, IFormCollection form)
        {
            string danieValue = form["Danie"].ToString();
            if (ModelState.IsValid || true)
            {
                Uzytkownik uzytkownik = null;

                var uu = _context.Uzytkownik.Where(x=>x.UzytkownikId == HttpContext.Session.GetInt32("UserID"));
                if(uu.Count() > 0) uzytkownik = uu.First();
                
                Danie danie = null;
                if(danieValue != "-1")
                {
                    var dd = _context.Danie.Where(x=>x.DanieId == int.Parse(danieValue));
                    if (dd.Count() > 0) danie = dd.First();
                }

                planDnia.Uzytkownik = uzytkownik;
                planDnia.Danie = danie;

                _context.Add(planDnia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planDnia);
        }

        // GET: PlanDnia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var username = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }
            var userID = HttpContext.Session.GetInt32("UserID");
            var cc = _context.PlanDnia
                    .Include(x => x.Uzytkownik)
                    .Include(x => x.Danie)
                    .Where(x => x.PlanDniaId == id && x.Uzytkownik.UzytkownikId == userID)
                    .FirstOrDefault();
            if (cc == null) return RedirectToAction("Index");
            if (id == null || _context.PlanDnia == null)
            {
                return NotFound();
            }

            //var planDnia = await _context.PlanDnia.FindAsync(id);
            var planDnia = _context.PlanDnia.Where(p=>p.PlanDniaId == id).Include(p=>p.Danie).Include(p=>p.Uzytkownik).First();
            if (planDnia == null)
            {
                return NotFound();
            }
            if (planDnia.Danie != null)
            {
                PopulateDaniaDropDownList(planDnia.Danie.DanieId);
            }
            else
            {
                PopulateDaniaDropDownList();
            }
            ViewData["ListaDan"] = _context.Danie.ToList();
            return View(planDnia);
        }

        // POST: PlanDnia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanDniaId,DataDnia")] PlanDnia planDnia, IFormCollection form)
        {
            if (id != planDnia.PlanDniaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid || true)
            {
                try
                {
                    String danieValue = form["Danie"];
                    Uzytkownik uzytkownik = null;

                    var uu = _context.Uzytkownik.Where(x=>x.UzytkownikId == HttpContext.Session.GetInt32("UserID"));
                    if(uu.Count() > 0) uzytkownik = uu.First();
                    
                    Danie danie = null;
                    if(danieValue != "-1")
                    {
                        var dd = _context.Danie.Where(x=>x.DanieId == int.Parse(danieValue));
                        if (dd.Count() > 0) danie = dd.First();
                    }
                    planDnia.Danie = danie;
                    planDnia.Uzytkownik = uzytkownik;
                    //_context.Update(planDnia);
                    PlanDnia pd = _context.PlanDnia.Where(x=>x.PlanDniaId == id).Include(x=>x.Danie).Include(x=>x.Uzytkownik).First();
                    pd.Danie = danie;
                    pd.DataDnia = planDnia.DataDnia;
                    pd.Uzytkownik = uzytkownik;
                     
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanDniaExists(planDnia.PlanDniaId))
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
            return View(planDnia);
        }

        // GET: PlanDnia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var username = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }
            var userID = HttpContext.Session.GetInt32("UserID");
            var cc = _context.PlanDnia
                    .Include(x => x.Uzytkownik)
                    .Include(x => x.Danie)
                    .Where(x => x.PlanDniaId == id && x.Uzytkownik.UzytkownikId == userID)
                    .FirstOrDefault();
            if (cc == null) return RedirectToAction("Index");
            if (id == null || _context.PlanDnia == null)
            {
                return NotFound();
            }

            var planDnia = _context.PlanDnia.Where(p=>p.PlanDniaId == id)
                .Include(p=>p.Danie).Include(p=>p.Uzytkownik).First();

            //var planDnia = await _context.PlanDnia
            //    .FirstOrDefaultAsync(m => m.PlanDniaId == id);
            if (planDnia == null)
            {
                return NotFound();
            }

            return View(planDnia);
        }

        // POST: PlanDnia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlanDnia == null)
            {
                return Problem("Entity set 'ProjektContext.PlanDnia'  is null.");
            }
            var planDnia = await _context.PlanDnia.FindAsync(id);
            if (planDnia != null)
            {
                _context.PlanDnia.Remove(planDnia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanDniaExists(int id)
        {
          return (_context.PlanDnia?.Any(e => e.PlanDniaId == id)).GetValueOrDefault();
        }
    }
}
