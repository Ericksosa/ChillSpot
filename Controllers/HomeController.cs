using ChillSpot.Data;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index(string email, string password)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == email && u.Clave == password);
            if (usuario != null)
            {
                TempData["Nombre"] = usuario.Nombre;
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
