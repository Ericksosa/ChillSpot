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
                // Valida el modelo explícitamente (si ya usas [Required] en el modelo,
                // esto normalmente no es necesario pero lo dejo por seguridad)
                if (!TryValidateModel(tipoArticulo))
                {
                    ModelState.AddModelError(string.Empty, "Modelo inválido.");
                    throw new InvalidOperationException("Modelo inválido.");
                }

                _context.Add(tipoArticulo);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                // Mensaje de éxito que puedes mostrar en la vista/layout
                TempData["success"] = "Tipo de artículo creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

                // Mensaje de error para mostrar en la vista/layout
                TempData["danger"] = "Ocurrió un error al crear el tipo de artículo. Inténtalo nuevamente.";
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
                // Validación explícita
                if (!TryValidateModel(tipoArticulo))
                {
                    ModelState.AddModelError(string.Empty, "Modelo inválido.");
                    throw new InvalidOperationException("Modelo inválido.");
                }

                _context.Update(tipoArticulo);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                // Mensaje de éxito para mostrar en la vista/layout
                TempData["success"] = "Tipo de artículo editado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();

                if (!TipoArticuloExists(tipoArticulo.Id))
                {
                    return NotFound();
                }

                // Mensaje específico para concurrencia
                TempData["danger"] = "Conflicto de concurrencia al actualizar el tipo de artículo.";
                ModelState.AddModelError(string.Empty, "Conflicto de concurrencia al actualizar.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                // Mensaje genérico de error
                TempData["danger"] = "Ocurrió un error al editar el tipo de artículo. Inténtalo nuevamente.";
                ModelState.AddModelError(string.Empty, "Ocurrió un error al editar el tipo de artículo.");
                // Opcional: añadir el mensaje de la excepción (útil en desarrollo)
                ModelState.AddModelError(string.Empty, ex.Message);
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
                    TempData["danger"] = "El tipo de artículo no existe.";
                    return RedirectToAction(nameof(Index));
                }

                _context.TipoArticulos.Remove(tipoArticulo);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                TempData["success"] = "Tipo de artículo eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                TempData["danger"] = "Error al eliminar el tipo de artículo. Inténtalo nuevamente.";
                // Opcional: agregar detalle en ModelState (útil en desarrollo)
                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction(nameof(Index));
            }
        }

        private bool TipoArticuloExists(long id)
        {
            return _context.TipoArticulos.Any(e => e.Id == id);
        }
    }
}
