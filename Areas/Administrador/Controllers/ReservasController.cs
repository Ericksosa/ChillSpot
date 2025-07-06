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
    public class ReservasController : Controller
    {
        private readonly chillSpotDbContext _context;

        public ReservasController(chillSpotDbContext context)
        {
            _context = context;
        }

        // GET: Administrador/Reservas
        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.Reservas.Include(r => r.Articulo).Include(r => r.Cliente).Include(r => r.Empleado).Include(r => r.Estado).Include(r => r.IdDescuentoNavigation).Include(r => r.Penalizacion);
            return View(await chillSpotDbContext.ToListAsync());
        }

        // GET: Administrador/Reservas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Articulo)
                .Include(r => r.Cliente)
                .Include(r => r.Empleado)
                .Include(r => r.Estado)
                .Include(r => r.IdDescuentoNavigation)
                .Include(r => r.Penalizacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Administrador/Reservas/Create
        public IActionResult Create()
        {
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id");
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id");
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id");
            ViewData["IdDescuento"] = new SelectList(_context.Descuentos, "Id", "Id");
            ViewData["PenalizacionId"] = new SelectList(_context.Penalizacions, "Id", "Id");
            return View();
        }

        // POST: Administrador/Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArticuloId,EstadoId,ClienteId,FechaCreacion,IdDescuento,DuracionReserva,EmpleadoId,MontoTotal,PenalizacionId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id", reserva.ArticuloId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", reserva.ClienteId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", reserva.EmpleadoId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", reserva.EstadoId);
            ViewData["IdDescuento"] = new SelectList(_context.Descuentos, "Id", "Id", reserva.IdDescuento);
            ViewData["PenalizacionId"] = new SelectList(_context.Penalizacions, "Id", "Id", reserva.PenalizacionId);
            return View(reserva);
        }

        // GET: Administrador/Reservas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id", reserva.ArticuloId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", reserva.ClienteId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", reserva.EmpleadoId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", reserva.EstadoId);
            ViewData["IdDescuento"] = new SelectList(_context.Descuentos, "Id", "Id", reserva.IdDescuento);
            ViewData["PenalizacionId"] = new SelectList(_context.Penalizacions, "Id", "Id", reserva.PenalizacionId);
            return View(reserva);
        }

        // POST: Administrador/Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ArticuloId,EstadoId,ClienteId,FechaCreacion,IdDescuento,DuracionReserva,EmpleadoId,MontoTotal,PenalizacionId")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id", reserva.ArticuloId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", reserva.ClienteId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", reserva.EmpleadoId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", reserva.EstadoId);
            ViewData["IdDescuento"] = new SelectList(_context.Descuentos, "Id", "Id", reserva.IdDescuento);
            ViewData["PenalizacionId"] = new SelectList(_context.Penalizacions, "Id", "Id", reserva.PenalizacionId);
            return View(reserva);
        }

        // GET: Administrador/Reservas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Articulo)
                .Include(r => r.Cliente)
                .Include(r => r.Empleado)
                .Include(r => r.Estado)
                .Include(r => r.IdDescuentoNavigation)
                .Include(r => r.Penalizacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Administrador/Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(long id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
