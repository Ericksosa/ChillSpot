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
    [Area("Administrador")]
    [SessionAuthorize("1")]
    public class ElencoController : Controller
    {
        private readonly chillSpotDbContext _context;

        public ElencoController(chillSpotDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var elencos = _context.Elencos.Include(u => u.RolProfesional).AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                elencos = elencos.Where(u =>
                u.Nombre.Contains(searchString) ||
                u.Apellido.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(await elencos.ToListAsync());
        }

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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Si el modelo no es válido, hacemos rollback y devolvemos la vista
                    if (!ModelState.IsValid)
                    {
                        await transaction.RollbackAsync();
                        ViewData["RolProfesionalId"] = new SelectList(_context.RolProfesionals, "Id", "Nombre", elenco?.RolProfesionalId);
                        return View(elenco);
                    }

                    _context.Add(elenco);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    TempData["success"] = "Elenco creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    TempData["danger"] = "Error al guardar los datos. Inténtalo nuevamente.";
                    // Opcional: agregar detalle al ModelState para desarrollo
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
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

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Si el modelo no es válido devolvemos la vista con los errores
                if (!ModelState.IsValid)
                {
                    await transaction.RollbackAsync();
                    ViewData["RolProfesionalId"] = new SelectList(_context.RolProfesionals, "Id", "Nombre", elenco?.RolProfesionalId);
                    return View(elenco);
                }

                _context.Update(elenco);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                TempData["success"] = "Elenco editado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();

                if (!ElencoExists(elenco.Id))
                {
                    return NotFound();
                }

                TempData["danger"] = "Conflicto de concurrencia al actualizar el elenco.";
                ModelState.AddModelError(string.Empty, "Conflicto de concurrencia al actualizar.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                TempData["danger"] = "Ocurrió un error al editar el elenco. Inténtalo nuevamente.";
                ModelState.AddModelError(string.Empty, "Ocurrió un error al editar el elenco.");
                // útil en desarrollo:
                ModelState.AddModelError(string.Empty, ex.Message);
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
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var elenco = await _context.Elencos.FindAsync(id);
                if (elenco == null)
                {
                    TempData["danger"] = "El elenco no existe.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Elencos.Remove(elenco);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                TempData["success"] = "Elenco eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                TempData["danger"] = "Error al eliminar el elenco. Inténtalo nuevamente.";
                // Opcional: registrar el detalle en ModelState para debugging en desarrollo
                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction(nameof(Index));
            }
        }

        private bool ElencoExists(long id)
        {
            return _context.Elencos.Any(e => e.Id == id);
        }
    }
}
