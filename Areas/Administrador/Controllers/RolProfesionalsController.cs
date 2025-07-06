using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChillSpot.Data;
using ChillSpot.Models;
using ChillSpot.Filters;

namespace ChillSpot.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    [SessionAuthorize("1")]
    public class RolProfesionalsController : Controller
    {
        private readonly chillSpotDbContext _context;

        public RolProfesionalsController(chillSpotDbContext context)
        {
            _context = context;
        }

        // GET: Administrador/RolProfesionals
        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.RolProfesionals.Include(r => r.Estado);
            return View(await chillSpotDbContext.ToListAsync());
        }

        // GET: Administrador/RolProfesionals/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolProfesional = await _context.RolProfesionals
                .Include(r => r.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolProfesional == null)
            {
                return NotFound();
            }

            return View(rolProfesional);
        }

        // GET: Administrador/RolProfesionals/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id");
            return View();
        }

        // POST: Administrador/RolProfesionals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,EstadoId")] RolProfesional rolProfesional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolProfesional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", rolProfesional.EstadoId);
            return View(rolProfesional);
        }

        // GET: Administrador/RolProfesionals/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolProfesional = await _context.RolProfesionals.FindAsync(id);
            if (rolProfesional == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", rolProfesional.EstadoId);
            return View(rolProfesional);
        }

        // POST: Administrador/RolProfesionals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Descripcion,EstadoId")] RolProfesional rolProfesional)
        {
            if (id != rolProfesional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolProfesional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolProfesionalExists(rolProfesional.Id))
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
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", rolProfesional.EstadoId);
            return View(rolProfesional);
        }

        // GET: Administrador/RolProfesionals/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolProfesional = await _context.RolProfesionals
                .Include(r => r.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolProfesional == null)
            {
                return NotFound();
            }

            return View(rolProfesional);
        }

        // POST: Administrador/RolProfesionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var rolProfesional = await _context.RolProfesionals.FindAsync(id);
            if (rolProfesional != null)
            {
                _context.RolProfesionals.Remove(rolProfesional);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolProfesionalExists(long id)
        {
            return _context.RolProfesionals.Any(e => e.Id == id);
        }
    }
}
