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
    public class EstadosController : Controller
    {
        private readonly chillSpotDbContext _context;

        public EstadosController(chillSpotDbContext context)
        {
            _context = context;
        }

        // GET: Estados
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estados.ToListAsync());
        }

        // GET: Estados/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // GET: Estados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.Add(estado);
                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        TempData["success"] = "Estado creado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        TempData["danger"] = "Error al guardar los datos. Inténtalo nuevamente.";
                        ModelState.AddModelError("", "Error al guardar los datos. Inténtalo nuevamente.");
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }

            return View(estado);
        }

        // GET: Estados/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }
            return View(estado);
        }

        // POST: Estados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Descripcion")] Estado estado)
        {
            if (id != estado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.Update(estado);
                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        TempData["success"] = "Estado editado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        await transaction.RollbackAsync();

                        if (!EstadoExists(estado.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            TempData["danger"] = "Error de concurrencia al actualizar los datos.";
                            ModelState.AddModelError("", "Error de concurrencia al actualizar los datos.");
                        }
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        TempData["danger"] = "Error al guardar los datos. Inténtalo nuevamente.";
                        ModelState.AddModelError("", "Error al actualizar los datos. Inténtalo nuevamente.");
                        ModelState.AddModelError("", ex.Message);
                    }
                }
             
            }
            return View(estado);
        }

        // GET: Estados/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var estado = await _context.Estados.FindAsync(id);
                    if (estado == null)
                    {
                        TempData["danger"] = "El estado no existe o ya fue eliminado.";
                        return RedirectToAction(nameof(Index));
                    }

                    _context.Estados.Remove(estado);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    TempData["success"] = "Estado eliminado exitosamente.";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    TempData["danger"] = "Error al eliminar el estado. Inténtalo nuevamente.";
                    ModelState.AddModelError("", "Error al eliminar los datos. Inténtalo nuevamente.");
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EstadoExists(long id)
        {
            return _context.Estados.Any(e => e.Id == id);
        }
    }
}
