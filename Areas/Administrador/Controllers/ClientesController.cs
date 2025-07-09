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
    public class ClientesController : Controller
    {
        private readonly chillSpotDbContext _context;

        public ClientesController(chillSpotDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.Clientes.Include(c => c.Estado).Include(c => c.Usuario);
            return View(await chillSpotDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Estado)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(
                _context.Estados.Select(e => new { e.Id, Nombre = e.Id + " - " + e.Nombre }),
                "Id", "Nombre"
            );

            var usedUsuarioIdsClientes = _context.Clientes.Select(c => c.UsuarioId).ToList();
            var usedUsuarioIdsEmpleados = _context.Empleados.Select(e => e.UsuarioId).ToList();

            var availableUsuarios = _context.Usuarios
                .Where(u => !usedUsuarioIdsClientes.Contains(u.Id) && !usedUsuarioIdsEmpleados.Contains(u.Id))
                .Select(u => new { u.Id, Nombre = u.Id + " - " + u.Nombre })
                .ToList();

            if (!availableUsuarios.Any())
            {
                ViewData["NoUsuariosDisponibles"] = "No hay más usuarios disponibles para asignar a clientes.";
            }

            ViewData["UsuarioId"] = new SelectList(availableUsuarios, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cedula,TarjetaCr,LimiteCredito,TipoPersona,EstadoId,UsuarioId")] Models.Cliente cliente)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Add(cliente);
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

            ViewData["EstadoId"] = new SelectList(
                _context.Estados, "Id", "Nombre", cliente.EstadoId
            );
            ViewData["UsuarioId"] = new SelectList(
                _context.Usuarios.Select(u => new { u.Id, Nombre = u.Id + " - " + u.Nombre }),
                "Id", "Nombre", cliente.UsuarioId
            );

            return View(cliente);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            ViewData["EstadoId"] = new SelectList(
                _context.Estados.Select(e => new { e.Id, Nombre = e.Id + " - " + e.Nombre }),
                "Id", "Nombre", cliente.EstadoId
            );

            var usedUsuarioIdsClientes = _context.Clientes.Where(c => c.Id != id).Select(c => c.UsuarioId).ToList();
            var usedUsuarioIdsEmpleados = _context.Empleados.Select(e => e.UsuarioId).ToList();

            var availableUsuarios = _context.Usuarios
                .Where(u => !usedUsuarioIdsClientes.Contains(u.Id) && !usedUsuarioIdsEmpleados.Contains(u.Id))
                .Select(u => new { u.Id, Nombre = u.Id + " - " + u.Nombre })
                .ToList();

            if (!availableUsuarios.Any())
            {
                ViewData["NoUsuariosDisponibles"] = "No hay más usuarios disponibles para asignar a clientes.";
            }

            ViewData["UsuarioId"] = new SelectList(availableUsuarios, "Id", "Nombre", cliente.UsuarioId);

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Cedula,TarjetaCr,LimiteCredito,TipoPersona,EstadoId,UsuarioId")] Models.Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Update(cliente);
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

            ViewData["EstadoId"] = new SelectList(
                _context.Estados.Select(e => new { e.Id, Nombre = e.Id + " - " + e.Nombre }),
                "Id", "Nombre", cliente.EstadoId
            );
            ViewData["UsuarioId"] = new SelectList(
                _context.Usuarios.Select(u => new { u.Id, Nombre = u.Id + " - " + u.Nombre }),
                "Id", "Nombre", cliente.UsuarioId
            );

            return View(cliente);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Estado)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var cliente = await _context.Clientes.FindAsync(id);
                    if (cliente != null)
                    {
                        _context.Clientes.Remove(cliente);
                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    ModelState.AddModelError("", "No se pudo eliminar el cliente. Inténtalo nuevamente.");
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(long id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}