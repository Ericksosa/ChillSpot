using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChillSpot.Data;
using ChillSpot.Models;

namespace ChillSpot.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class PenalizacionsController : Controller
    {
        private readonly chillSpotDbContext _context;

        public PenalizacionsController(chillSpotDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.Penalizacions.Include(p => p.Estado);
            return View(await chillSpotDbContext.ToListAsync());
        }
       
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penalizacion = await _context.Penalizacions
                .Include(p => p.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penalizacion == null)
            {
                return NotFound();
            }

            return View(penalizacion);
        }
        
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Tipo,Monto,Descripcion,EstadoId")] Penalizacion penalizacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(penalizacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", penalizacion.EstadoId);
            return View(penalizacion);
        }

        
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penalizacion = await _context.Penalizacions.FindAsync(id);
            if (penalizacion == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", penalizacion.EstadoId);
            return View(penalizacion);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Tipo,Monto,Descripcion,EstadoId")] Penalizacion penalizacion)
        {
            if (id != penalizacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penalizacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenalizacionExists(penalizacion.Id))
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
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", penalizacion.EstadoId);
            return View(penalizacion);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penalizacion = await _context.Penalizacions
                .Include(p => p.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penalizacion == null)
            {
                return NotFound();
            }

            return View(penalizacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var penalizacion = await _context.Penalizacions.FindAsync(id);
            if (penalizacion != null)
            {
                _context.Penalizacions.Remove(penalizacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenalizacionExists(long id)
        {
            return _context.Penalizacions.Any(e => e.Id == id);
        }
    }
}
