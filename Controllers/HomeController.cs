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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == email && u.Clave == password);
            if (usuario != null)
            {
                // Crear los claims del usuario
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Nombre ?? ""),
            new Claim(ClaimTypes.Email, usuario.Correo ?? ""),
            new Claim(ClaimTypes.Role, usuario.RolId?.ToString() ?? "")
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                // Firmar al usuario (emitir la cookie)
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                TempData["Nombre"] = usuario.Nombre;
                if (usuario.RolId == 1)
                    return RedirectToAction("Index", "Home", new { area = "Administrador" });
                else if (usuario.RolId == 2)
                    return RedirectToAction("Index", "Home", new { area = "Cliente" });
            }

            ViewBag.Error = "Credenciales inválidas";
            return View();
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
