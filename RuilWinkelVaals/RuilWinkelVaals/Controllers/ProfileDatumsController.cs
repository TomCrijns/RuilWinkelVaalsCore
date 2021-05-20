using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RuilWinkelVaals.Models;

namespace RuilWinkelVaals.Controllers
{
    public class ProfileDatumsController : Controller
    {
        private readonly DB_DevOpsContext _context;

        public ProfileDatumsController(DB_DevOpsContext context)
        {
            _context = context;
        }

        // GET: ProfileDatums
        public async Task<IActionResult> Index()
        {
            var dB_DevOpsContext = _context.ProfileData.Include(p => p.AccountTypeNavigation).Include(p => p.LedenpasNavigation);
            return View(await dB_DevOpsContext.ToListAsync());
        }

        // GET: ProfileDatums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileDatum = await _context.ProfileData
                .Include(p => p.AccountTypeNavigation)
                .Include(p => p.LedenpasNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profileDatum == null)
            {
                return NotFound();
            }

            return View(profileDatum);
        }

        // GET: ProfileDatums/Create
        public IActionResult Create()
        {
            ViewData["AccountType"] = new SelectList(_context.AccountTypeLts, "Id", "Id");
            ViewData["Ledenpas"] = new SelectList(_context.LedenpasLts, "Id", "Id");
            return View();
        }

        // POST: ProfileDatums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Voornaam,Achternaam,Balans,AccountType,Ledenpas,Straat,Huisnummer,Woonplaats,Postcode,DateCreated,Geboortedatum")] ProfileDatum profileDatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profileDatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountType"] = new SelectList(_context.AccountTypeLts, "Id", "Id", profileDatum.AccountType);
            ViewData["Ledenpas"] = new SelectList(_context.LedenpasLts, "Id", "Id", profileDatum.Ledenpas);
            return View(profileDatum);
        }

        // GET: ProfileDatums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileDatum = await _context.ProfileData.FindAsync(id);
            if (profileDatum == null)
            {
                return NotFound();
            }
            ViewData["AccountType"] = new SelectList(_context.AccountTypeLts, "Id", "Id", profileDatum.AccountType);
            ViewData["Ledenpas"] = new SelectList(_context.LedenpasLts, "Id", "Id", profileDatum.Ledenpas);
            return View(profileDatum);
        }

        // POST: ProfileDatums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Voornaam,Achternaam,Balans,AccountType,Ledenpas,Straat,Huisnummer,Woonplaats,Postcode,DateCreated,Geboortedatum")] ProfileDatum profileDatum)
        {
            if (id != profileDatum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profileDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileDatumExists(profileDatum.Id))
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
            ViewData["AccountType"] = new SelectList(_context.AccountTypeLts, "Id", "Id", profileDatum.AccountType);
            ViewData["Ledenpas"] = new SelectList(_context.LedenpasLts, "Id", "Id", profileDatum.Ledenpas);
            return View(profileDatum);
        }

        // GET: ProfileDatums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileDatum = await _context.ProfileData
                .Include(p => p.AccountTypeNavigation)
                .Include(p => p.LedenpasNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profileDatum == null)
            {
                return NotFound();
            }

            return View(profileDatum);
        }

        // POST: ProfileDatums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profileDatum = await _context.ProfileData.FindAsync(id);
            _context.ProfileData.Remove(profileDatum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileDatumExists(int id)
        {
            return _context.ProfileData.Any(e => e.Id == id);
        }
    }
}
