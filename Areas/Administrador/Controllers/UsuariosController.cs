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
    public class UsuariosController : Controller
    {
        private readonly chillSpotDbContext _context;

        public UsuariosController(chillSpotDbContext context)
        {
            _context = context;
        }

       

        public async Task<IActionResult> Index(string searchString)
        {
            var usuarios = _context.Usuarios.Include(u => u.Rol).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                usuarios = usuarios.Where(u =>
                    u.Nombre.Contains(searchString) ||
                    u.Correo.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(await usuarios.ToListAsync());
        }

        // Acción GET: muestra el formulario
        public async Task<IActionResult> AsignarRol(long id)
        {
            var usuario = await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null) return NotFound();

            // Obtener todos los roles disponibles
            var roles = await _context.Rols.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Nombre", usuario.RolId);

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarRol(long? id, long rolId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var rol = await _context.Rols.FindAsync(rolId);
            if (rol == null)
            {
                ModelState.AddModelError("", "El rol seleccionado no existe.");
                ViewBag.Roles = new SelectList(_context.Rols, "Id", "Nombre", rolId);
                TempData["danger"] = "Error al guardar los datos. Inténtalo nuevamente.";
                return View(usuario);
            }

            usuario.RolId = rolId;
            _context.Update(usuario);
            await _context.SaveChangesAsync();
            TempData["success"] = "Rol asignado exitosamente.";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        public IActionResult Create()
        {
            ViewData["RolId"] = new SelectList(_context.Rols, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Correo,Clave,RolId")] Usuario usuario)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["success"] = "Usuario creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    TempData["danger"] = "Error al guardar los datos. Inténtalo nuevamente.";
                    ModelState.AddModelError("", "Error al guardar los datos. Inténtalo nuevamente.");
                    ModelState.AddModelError("", ex.InnerException?.Message ?? ex.Message);
                }
            }
            ViewData["RolId"] = new SelectList(_context.Rols, "Id", "Nombre", usuario.RolId);
            return View(usuario);
        }


        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["RolId"] = new SelectList(_context.Rols, "Id", "Nombre", usuario.RolId);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Correo,Clave,RolId")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["success"] = "Usuario editado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error de concurrencia al actualizar el usuario.");
                    }
                }
                catch (Exception ex)
                {
                    TempData["danger"] = "Error al guardar los datos. Inténtalo nuevamente.";
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Error al actualizar los datos. Inténtalo nuevamente.");
                    ModelState.AddModelError("", ex.InnerException?.Message ?? ex.Message);
                }
            }

            ViewData["RolId"] = new SelectList(_context.Rols, "Id", "Nombre", usuario.RolId);
            return View(usuario);
        }


        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var usuario = await _context.Usuarios.FindAsync(id);
                    if (usuario != null)
                    {
                        _context.Usuarios.Remove(usuario);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        TempData["success"] = "Usuario eliminado exitosamente.";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    TempData["danger"] = "Error al eliminar el usuario. Inténtalo nuevamente.";
                    ModelState.AddModelError("", "Error al eliminar el usuario. Inténtalo nuevamente.");
                    ModelState.AddModelError("", ex.InnerException?.Message ?? ex.Message);
                }
            }
            // Si hubo error, intenta mostrar la vista de confirmación nuevamente
            var usuarioError = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(usuarioError);
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

       
    }
}
