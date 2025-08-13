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
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre");
            return View();
        }

        // POST: Administrador/RolProfesionals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,EstadoId")] RolProfesional rolProfesional)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Add(rolProfesional);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["success"] = "Rol profesional creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    TempData["danger"] = "Error al guardar los datos. Inténtalo nuevamente.";
                }
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", rolProfesional.EstadoId);
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
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", rolProfesional.EstadoId);
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

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Update(rolProfesional);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["success"] = "Rol profesional editado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    TempData["danger"] = "Error al guardar los datos. Inténtalo nuevamente.";
                    ModelState.AddModelError("", "Error al actualizar los datos. Inténtalo nuevamente.");
                    ModelState.AddModelError("", ex.Message);
                }
                ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", rolProfesional.EstadoId);
                return View(rolProfesional);
            }
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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var rolProfesional = await _context.RolProfesionals.FindAsync(id);
                    if (rolProfesional != null)
                    {
                        _context.RolProfesionals.Remove(rolProfesional);
                        await _context.SaveChangesAsync();
                        TempData["success"] = "Rol profesional eliminado exitosamente.";
                        await transaction.CommitAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    TempData["danger"] = "Error al borrar el rol profesional. Inténtalo nuevamente.";
                    ModelState.AddModelError("", "No se pudo eliminar el rol profesional. Inténtalo nuevamente.");
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        

        private bool RolProfesionalExists(long id)
        {
            return _context.RolProfesionals.Any(e => e.Id == id);
        }
    }
}
