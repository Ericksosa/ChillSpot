using ChillSpot.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ChillSpot.Controllers
{
    public class HomeController : Controller
    {
        private readonly chillSpotDbContext _context;

        public HomeController(chillSpotDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Si el usuario ya está autenticado, redirige según el rol
            var autenticado = HttpContext.Session.GetString("UsuarioAutenticado");
            var rol = HttpContext.Session.GetString("Rol");

            if (autenticado == "true" && !string.IsNullOrEmpty(rol))
            {
                if (rol == "1")
                    return RedirectToAction("Index", "Home", new { area = "Administrador" });
                else if (rol == "2")
                    return RedirectToAction("Index", "Home", new { area = "Cliente" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == email && u.Clave == password);

            if (usuario != null)
            {
                // Guardar datos en sesión
                HttpContext.Session.SetString("UsuarioAutenticado", "true");
                HttpContext.Session.SetString("Rol", usuario.RolId?.ToString() ?? "");
                HttpContext.Session.SetString("Nombre", usuario.Nombre ?? "");
                if (usuario.RolId == 1)
                    return RedirectToAction("Index", "Home", new { area = "Administrador" });
                else if (usuario.RolId == 2)
                    return RedirectToAction("Index", "Home", new { area = "Cliente" });
            }

            ViewBag.Error = "Credenciales inválidas";
            return View();
        }
    }
}
