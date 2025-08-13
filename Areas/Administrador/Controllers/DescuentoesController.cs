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
    public class DescuentoesController : Controller
    {
        private readonly chillSpotDbContext _context;

        public DescuentoesController(chillSpotDbContext context)
        {
            _context = context;
        }

        // GET: Administrador/Descuentoes
        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.Descuentos.Include(d => d.EmpleadoCreador).Include(d => d.Estado);
            return View(await chillSpotDbContext.ToListAsync());
        }

        // GET: Administrador/Descuentoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuentos
                .Include(d => d.EmpleadoCreador)
                .Include(d => d.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (descuento == null)
            {
                return NotFound();
            }

            return View(descuento);
        }

        // GET: Administrador/Descuentoes/Create
        public IActionResult Create()
        {
            // Filtra empleados cuyo usuario tiene RolId == 1
            var empleadosConRol1 = _context.Empleados
                .Where(e => e.Usuario != null && e.Usuario.RolId == 1)
                .ToList();

            ViewData["EmpleadoCreadorId"] = new SelectList(empleadosConRol1, "Id", "Nombre");
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre");
            return View();
        }


        // POST: Administrador/Descuentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,MontoCobertura,EmpleadoCreadorId,EstadoId,FechaInicio,FechaVencimiento")] Descuento descuento)
        {
            try
            {
                _context.Add(descuento);
                await _context.SaveChangesAsync();
                TempData["success"] = "Descuento creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["danger"] = "Error al crear el descuento: " + ex.Message;
                ViewData["EmpleadoCreadorId"] = new SelectList(_context.Empleados, "Id", "Nombre", descuento.EmpleadoCreadorId);
                ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", descuento.EstadoId);
                return View(descuento);
            }
        }


        // GET: Administrador/Descuentoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuentos.FindAsync(id);
            if (descuento == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoCreadorId"] = new SelectList(_context.Empleados, "Id", "Nombre", descuento.EmpleadoCreadorId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", descuento.EstadoId);
            return View(descuento);
        }

        // POST: Administrador/Descuentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Descripcion,MontoCobertura,EmpleadoCreadorId,EstadoId,FechaInicio,FechaVencimiento")] Descuento descuento)
        {
            if (id != descuento.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(descuento);
                await _context.SaveChangesAsync();
                TempData["success"] = "Descuento editado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DescuentoExists(descuento.Id))
                {
                    return NotFound();
                }
                else
                {
                    TempData["danger"] = "Error de concurrencia al editar el descuento.";
                }
            }
            catch (Exception ex)
            {
                TempData["danger"] = "Error al editar el descuento: " + ex.Message;
            }

            ViewData["EmpleadoCreadorId"] = new SelectList(_context.Empleados, "Id", "Nombre", descuento.EmpleadoCreadorId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", descuento.EstadoId);
            return View(descuento);
        }


        // GET: Administrador/Descuentoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuentos
                .Include(d => d.EmpleadoCreador)
                .Include(d => d.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (descuento == null)
            {
                return NotFound();
            }

            return View(descuento);
        }

        // POST: Administrador/Descuentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var descuento = await _context.Descuentos.FindAsync(id);
                if (descuento != null)
                {
                    _context.Descuentos.Remove(descuento);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Descuento eliminado exitosamente.";
                }
                else
                {
                    TempData["danger"] = "No se encontró el descuento para eliminar.";
                }
            }
            catch (Exception ex)
            {
                TempData["danger"] = "Error al eliminar el descuento: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }


        private bool DescuentoExists(long id)
        {
            return _context.Descuentos.Any(e => e.Id == id);
        }
    }
}
