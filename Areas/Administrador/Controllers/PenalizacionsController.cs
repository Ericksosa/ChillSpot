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
    public class PenalizacionsController : Controller
    {
        private readonly chillSpotDbContext _context;

        public PenalizacionsController(chillSpotDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.Penalizacions.Include(p => p.Estado);
            return View(await chillSpotDbContext.ToListAsync());
        }
       
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penalizacion = await _context.Penalizacions
                .Include(p => p.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penalizacion == null)
            {
                return NotFound();
            }

            return View(penalizacion);
        }

        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Tipo,Monto,Descripcion,EstadoId")] Penalizacion penalizacion)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(penalizacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", penalizacion.EstadoId);
            return View(penalizacion);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penalizacion = await _context.Penalizacions.FindAsync(id);
            if (penalizacion == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", penalizacion.EstadoId);
            return View(penalizacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Tipo,Monto,Descripcion,EstadoId")] Penalizacion penalizacion)
        {
            if (id != penalizacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penalizacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenalizacionExists(penalizacion.Id))
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
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id", penalizacion.EstadoId);
            return View(penalizacion);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penalizacion = await _context.Penalizacions
                .Include(p => p.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penalizacion == null)
            {
                return NotFound();
            }

            return View(penalizacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var penalizacion = await _context.Penalizacions.FindAsync(id);
            if (penalizacion != null)
            {
                _context.Penalizacions.Remove(penalizacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenalizacionExists(long id)
        {
            return _context.Penalizacions.Any(e => e.Id == id);
        }



        public async Task<IActionResult> AsignarAReserva(long? id)
        {
            if (id == null)
                return NotFound();

            // Solo reservas activas
            var reservas = await _context.Reservas
                .Where(r => r.EstadoId == 1)
                .Include(r => r.Cliente)
                .ToListAsync();

            var reservaList = reservas.Select(r => new
            {
                Id = r.Id,
                Display = $"Reserva #{r.Id} - Cliente: {(r.Cliente != null ? r.Cliente.Nombre : "Sin cliente")}"
            });

            ViewData["ReservaId"] = new SelectList(reservaList, "Id", "Display");
            ViewData["PenalizacionId"] = id; // Para mantener el id de penalización
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarAReserva(long ReservaId, long PenalizacionId)
        {
            var reserva = await _context.Reservas.FirstOrDefaultAsync(r => r.Id == ReservaId && r.EstadoId == 1);
            if (reserva == null)
            {
                TempData["Error"] = "Reserva no encontrada o inactiva.";
                return RedirectToAction(nameof(AsignarAReserva), new { id = PenalizacionId });
            }

            reserva.PenalizacionId = PenalizacionId;
            _context.Update(reserva);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Penalización asociada correctamente a la reserva.";
            return RedirectToAction("Index", new { id = PenalizacionId });
        }


    }
}
