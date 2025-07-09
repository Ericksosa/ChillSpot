using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChillSpot.Data;
using ChillSpot.Models;
using Microsoft.AspNetCore.Authorization;
using ChillSpot.Filters;

namespace ChillSpot.Areas.Administrador.Controllers
{
    [SessionAuthorize("1")]
    [Area("Administrador")]
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
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,EstadoId,Descripcion")] TipoArticulo tipoArticulo)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (!TryValidateModel(tipoArticulo))
                {
                    throw new InvalidOperationException("Modelo inválido.");
                }

                _context.Add(tipoArticulo);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear el tipo de artículo.");
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", tipoArticulo.EstadoId);
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
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", tipoArticulo.EstadoId);
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

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (!TryValidateModel(tipoArticulo))
                {
                    throw new InvalidOperationException("Modelo inválido.");
                }

                _context.Update(tipoArticulo);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();

                if (!TipoArticuloExists(tipoArticulo.Id))
                {
                    return NotFound();
                }

                ModelState.AddModelError(string.Empty, "Conflicto de concurrencia al actualizar.");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "Ocurrió un error al editar el tipo de artículo.");
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", tipoArticulo.EstadoId);
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
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var tipoArticulo = await _context.TipoArticulos.FindAsync(id);
                if (tipoArticulo == null)
                {
                    return NotFound();
                }

                _context.TipoArticulos.Remove(tipoArticulo);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "Ocurrió un error al eliminar el tipo de artículo.");

                var tipoArticulo = await _context.TipoArticulos
                    .Include(t => t.Estado)
                    .FirstOrDefaultAsync(t => t.Id == id);
                return tipoArticulo == null ? NotFound() : View("Delete", tipoArticulo);
            }
        }

        private bool TipoArticuloExists(long id)
        {
            return _context.TipoArticulos.Any(e => e.Id == id);
        }
    }
}
