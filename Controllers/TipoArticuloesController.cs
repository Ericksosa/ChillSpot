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
    public class TipoArticuloesController : Controller
    {
        private readonly chillSpotDbContext _context;

        public TipoArticuloesController(chillSpotDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.TipoArticulos.Include(t => t.Estado);
            return View(await chillSpotDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoArticulo = await _context.TipoArticulos
                .Include(t => t.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoArticulo == null)
            {
                return NotFound();
            }

            return View(tipoArticulo);
        }

        
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,EstadoId,Descripcion")] TipoArticulo tipoArticulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoArticulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", tipoArticulo.EstadoId);
            return View(tipoArticulo);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoArticulo = await _context.TipoArticulos.FindAsync(id);
            if (tipoArticulo == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", tipoArticulo.EstadoId);
            return View(tipoArticulo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,EstadoId,Descripcion")] TipoArticulo tipoArticulo)
        {
            if (id != tipoArticulo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoArticulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoArticuloExists(tipoArticulo.Id))
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
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", tipoArticulo.EstadoId);
            return View(tipoArticulo);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoArticulo = await _context.TipoArticulos
                .Include(t => t.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoArticulo == null)
            {
                return NotFound();
            }

            return View(tipoArticulo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoArticulo = await _context.TipoArticulos.FindAsync(id);
            if (tipoArticulo != null)
            {
                _context.TipoArticulos.Remove(tipoArticulo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoArticuloExists(long id)
        {
            return _context.TipoArticulos.Any(e => e.Id == id);
        }
    }
}
