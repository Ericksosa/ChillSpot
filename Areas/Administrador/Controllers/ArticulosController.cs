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
    public class ArticulosController : Controller
    {
        private readonly chillSpotDbContext _context;

        public ArticulosController(chillSpotDbContext context)
        {
            _context = context;
        }

        // GET: Administrador/Articulos
        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.Articulos.Include(a => a.Estado).Include(a => a.Genero).Include(a => a.Idioma).Include(a => a.TipoArticulo);
            return View(await chillSpotDbContext.ToListAsync());
        }

        // GET: Administrador/Articulos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .Include(a => a.Estado)
                .Include(a => a.Genero)
                .Include(a => a.Idioma)
                .Include(a => a.TipoArticulo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // GET: Administrador/Articulos/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre");
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre");
            ViewData["IdiomaId"] = new SelectList(_context.Idiomas, "Id", "Nombre");
            ViewData["TipoArticuloId"] = new SelectList(_context.TipoArticulos, "Id", "Nombre");
            return View();
        }

        // POST: Administrador/Articulos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,TipoArticuloId,IdiomaId,RentaXdia,MontoEntregaTardia,EstadoId,GeneroId,Sinopsis,AnioEstreno,PortadaUrl,RutaUrl")] Articulo articulo)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (!TryValidateModel(articulo))
                {
                    throw new InvalidOperationException("Modelo inválido.");
                }

                _context.Add(articulo);
                await _context.SaveChangesAsync();
                TempData["success"] = "Articulo creado exitosamente.";
                await transaction.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                TempData["danger"] = "Error al guardar los datos. Inténtalo nuevamente.";
                ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar el artículo.");
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", articulo.EstadoId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", articulo.GeneroId);
            ViewData["IdiomaId"] = new SelectList(_context.Idiomas, "Id", "Nombre", articulo.IdiomaId);
            ViewData["TipoArticuloId"] = new SelectList(_context.TipoArticulos, "Id", "Nombre", articulo.TipoArticuloId);
            return View(articulo);
        }

        // GET: Administrador/Articulos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", articulo.EstadoId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", articulo.GeneroId);
            ViewData["IdiomaId"] = new SelectList(_context.Idiomas, "Id", "Nombre", articulo.IdiomaId);
            ViewData["TipoArticuloId"] = new SelectList(_context.TipoArticulos, "Id", "Nombre", articulo.TipoArticuloId);
            return View(articulo);
        }

        // POST: Administrador/Articulos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Titulo,TipoArticuloId,IdiomaId,RentaXdia,MontoEntregaTardia,EstadoId,GeneroId,Sinopsis,AnioEstreno,PortadaUrl,RutaUrl")] Articulo articulo)
        {
            if (id != articulo.Id)
            {
                return NotFound();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (!TryValidateModel(articulo))
                {
                    throw new InvalidOperationException("Modelo inválido.");
                }

                _context.Update(articulo);
                await _context.SaveChangesAsync();
                TempData["success"] = "Articulo editado exitosamente.";
                await transaction.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                TempData["danger"] = "Error al guardar los datos. Inténtalo nuevamente.";
                if (!ArticuloExists(articulo.Id))
                {
                    return NotFound();
                }

                ModelState.AddModelError(string.Empty, "Conflicto de concurrencia al actualizar el artículo.");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar editar el artículo.");
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", articulo.EstadoId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", articulo.GeneroId);
            ViewData["IdiomaId"] = new SelectList(_context.Idiomas, "Id", "Nombre", articulo.IdiomaId);
            ViewData["TipoArticuloId"] = new SelectList(_context.TipoArticulos, "Id", "Nombre", articulo.TipoArticuloId);
            return View(articulo);
        }

        // GET: Administrador/Articulos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .Include(a => a.Estado)
                .Include(a => a.Genero)
                .Include(a => a.Idioma)
                .Include(a => a.TipoArticulo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // POST: Administrador/Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var articulo = await _context.Articulos
                    .Include(a => a.Estado)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (articulo == null)
                {
                    return NotFound();
                }

                if (articulo.Estado?.Nombre == "Reservado")
                {
                    ModelState.AddModelError(string.Empty, "No puedes eliminar este artículo porque está reservado o en uso.");
                    return View("Delete", articulo);
                }

                _context.Articulos.Remove(articulo);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                TempData["success"] = "Artículo eliminado exitosamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                TempData["danger"] = "Error al eliminar el artículo. Inténtalo nuevamente.";
                ModelState.AddModelError(string.Empty, "Ocurrió un error al eliminar el artículo.");
                var articulo = await _context.Articulos
                    .Include(a => a.Estado)
                    .Include(a => a.Genero)
                    .Include(a => a.Idioma)
                    .Include(a => a.TipoArticulo)
                    .FirstOrDefaultAsync(a => a.Id == id);

                return articulo == null ? NotFound() : View("Delete", articulo);
            }
        }
        private bool ArticuloExists(long id)
        {
            return _context.Articulos.Any(e => e.Id == id);
        }
    }
}
