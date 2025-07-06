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
    public class EmpleadosController : Controller
    {
        private readonly chillSpotDbContext _context;

        public EmpleadosController(chillSpotDbContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.Empleados.Include(e => e.Estado).Include(e => e.Usuario);
            return View(await chillSpotDbContext.ToListAsync());
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Estado)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre");

            var usedUsuarioIds = _context.Clientes.Select(c => c.UsuarioId).ToList();
            var usedEmpleadoIds = _context.Empleados.Select(e => e.UsuarioId).ToList();

            var availableUsuarios = _context.Usuarios
                .Where(u => !usedUsuarioIds.Contains(u.Id) && !usedEmpleadoIds.Contains(u.Id))
                .ToList();

            if (!availableUsuarios.Any())
            {
                ViewData["NoUsuariosDisponibles"] = "No hay más usuarios disponibles para asignar a empleados.";
            }

            ViewData["UsuarioId"] = new SelectList(availableUsuarios, "Id", "Nombre");

            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cedula,TandaLabor,PorcientoComision,FechaIngreso,EstadoId,UsuarioId")] Empleado empleado)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Add(empleado);
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); 

                    ModelState.AddModelError("", "Ocurrió un error al guardar los datos. Intenta nuevamente.");
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", empleado.EstadoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", empleado.UsuarioId);

            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", empleado.EstadoId);

            var usedUsuarioIds = _context.Clientes.Select(c => c.UsuarioId).ToList();
            var usedEmpleadoIds = _context.Empleados.Where(e => e.Id != id).Select(e => e.UsuarioId).ToList();
            var availableUsuarios = _context.Usuarios
                .Where(u => !usedUsuarioIds.Contains(u.Id) && !usedEmpleadoIds.Contains(u.Id))
                .ToList();

            if (!availableUsuarios.Any())
            {
                ViewData["NoUsuariosDisponibles"] = "No hay más usuarios disponibles para asignar a empleados.";
            }

            ViewData["UsuarioId"] = new SelectList(availableUsuarios, "Id", "Nombre", empleado.UsuarioId);

            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Cedula,TandaLabor,PorcientoComision,FechaIngreso,EstadoId,UsuarioId")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();

                    transaction.Commit(); 

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); 

                    ModelState.AddModelError("", "Ocurrió un error al actualizar los datos. Intenta nuevamente.");
                    ModelState.AddModelError("", ex.Message); 
                }
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", empleado.EstadoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", empleado.UsuarioId);

            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Estado)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var empleado = await _context.Empleados.FindAsync(id);
                    if (empleado != null)
                    {
                        _context.Empleados.Remove(empleado);
                        await _context.SaveChangesAsync();

                        transaction.Commit(); 
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    ModelState.AddModelError("", "No se pudo eliminar el empleado. Intenta nuevamente.");
                    ModelState.AddModelError("", ex.Message); 
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(long id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
