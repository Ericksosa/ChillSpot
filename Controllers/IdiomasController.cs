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
    public class IdiomasController : Controller
    {
        private readonly chillSpotDbContext _context;

        public IdiomasController(chillSpotDbContext context)
        {
            _context = context;
        }

        // GET: Idiomas
        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.Idiomas.Include(i => i.Estado);
            return View(await chillSpotDbContext.ToListAsync());
        }

        // GET: Idiomas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idioma = await _context.Idiomas
                .Include(i => i.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idioma == null)
            {
                return NotFound();
            }

            return View(idioma);
        }

        // GET: Idiomas/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre");
            return View();
        }

        // POST: Idiomas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,EstadoId")] Idioma idioma)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Add(idioma);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync(); 

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(); 

                    ModelState.AddModelError("", "Error al guardar los datos. Inténtalo nuevamente.");
                    ModelState.AddModelError("", ex.Message); 
                }
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", idioma.EstadoId);

            return View(idioma);
        }

        // GET: Idiomas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idioma = await _context.Idiomas.FindAsync(id);
            if (idioma == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", idioma.EstadoId);
            return View(idioma);
        }

        // POST: Idiomas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Descripcion,EstadoId")] Idioma idioma)
        {
            if (id != idioma.Id)
            {
                return NotFound();
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Update(idioma);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    ModelState.AddModelError("", "Error al actualizar los datos. Inténtalo nuevamente.");
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", idioma.EstadoId);

            return View(idioma);
        }

        // GET: Idiomas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idioma = await _context.Idiomas
                .Include(i => i.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idioma == null)
            {
                return NotFound();
            }

            return View(idioma);
        }

        // POST: Idiomas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var idioma = await _context.Idiomas.FindAsync(id);
                    if (idioma != null)
                    {
                        _context.Idiomas.Remove(idioma);
                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync(); // Confirma eliminación si todo va bien
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(); // Revierte cambios en caso de error

                    ModelState.AddModelError("", "No se pudo eliminar el idioma. Inténtalo nuevamente.");
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool IdiomaExists(long id)
        {
            return _context.Idiomas.Any(e => e.Id == id);
        }
    }
}
