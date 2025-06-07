using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChillSpot.Data;
using ChillSpot.Models;

namespace ChillSpot.Controllers
{
    public class ElencoController : Controller
    {
        private readonly chillSpotDbContext _context;

        public ElencoController(chillSpotDbContext context)
        {
            _context = context;
        }

        // GET: Elenco
        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.Elencos.Include(e => e.RolProfesional);
            return View(await chillSpotDbContext.ToListAsync());
        }

        // GET: Elenco/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elenco = await _context.Elencos
                .Include(e => e.RolProfesional)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (elenco == null)
            {
                return NotFound();
            }

            return View(elenco);
        }

        // GET: Elenco/Create
        public IActionResult Create()
        {
            ViewData["RolProfesionalId"] = new SelectList(_context.RolProfesionals, "Id", "Nombre");
            return View();
        }

        // POST: Elenco/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,FechaNacimiento,RolProfesionalId")] Elenco elenco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(elenco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RolProfesionalId"] = new SelectList(_context.RolProfesionals, "Id", "Nombre", elenco?.RolProfesionalId);
            return View(elenco);
        }

        // GET: Elenco/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elenco = await _context.Elencos.FindAsync(id);
            if (elenco == null)
            {
                return NotFound();
            }
            ViewData["RolProfesionalId"] = new SelectList(_context.RolProfesionals, "Id", "Nombre", elenco?.RolProfesionalId);
            return View(elenco);
        }

        // POST: Elenco/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Apellido,FechaNacimiento,RolProfesionalId")] Elenco elenco)
        {
            if (id != elenco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(elenco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElencoExists(elenco.Id))
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
            ViewData["RolProfesionalId"] = new SelectList(_context.RolProfesionals, "Id", "Nombre", elenco?.RolProfesionalId);
            return View(elenco);
        }

        // GET: Elenco/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elenco = await _context.Elencos
                .Include(e => e.RolProfesional)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (elenco == null)
            {
                return NotFound();
            }

            return View(elenco);
        }

        // POST: Elenco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var elenco = await _context.Elencos.FindAsync(id);
            if (elenco != null)
            {
                _context.Elencos.Remove(elenco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElencoExists(long id)
        {
            return _context.Elencos.Any(e => e.Id == id);
        }
    }
}
