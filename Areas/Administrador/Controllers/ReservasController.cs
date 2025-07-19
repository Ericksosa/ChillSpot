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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;

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
            var chillSpotDbContext = _context.Reservas.Include(r => r.Articulo).Include(r => r.Cliente).Include(r => r.Empleado).Include(r => r.Estado).Include(r => r.IdDescuentoNavigation).Include(r => r.Penalizacion).Where(r => r.EstadoId == 1);
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
                .FirstOrDefaultAsync(m => m.Id == id && m.EstadoId == 1);
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

            var reserva = await _context.Reservas.FirstOrDefaultAsync(r => r.Id == id && r.EstadoId == 1); 
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
                .FirstOrDefaultAsync(m => m.Id == id && m.EstadoId == 1);
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

        // GET: Administrador/Reservas/ExportPdfITextSharp
        public async Task<IActionResult> ExportPdfITextSharp()
        {
            // 1. Obtener datos
            var reservas = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Articulo)
                .Include(r => r.Empleado)
                .Where(r => r.EstadoId == 1)
                .ToListAsync();

            // 2. Generar PDF en memoria
            using var ms = new MemoryStream();
            var doc = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);
            PdfWriter.GetInstance(doc, ms);
            doc.Open();

            // 3. Título
            var titleFont = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 16);
            var title = new Paragraph("Listado de Reservas Activas", titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20
            };
            doc.Add(title);

            // 4. Tabla con 6 columnas
            var table = new PdfPTable(6) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 1f, 3f, 3f, 3f, 3f, 2f });

            foreach (var r in reservas)
            {
                // ID
                AddCell(table, r.Id.ToString());

                // Nombre de cliente, artículo y empleado (con null-coalescing)
                AddCell(table, r.Cliente?.Nombre ?? "—");
                AddCell(table, r.Articulo?.Titulo ?? "—");
                AddCell(table, r.Empleado?.Nombre ?? "—");

                // FechaCreacion como DateTime?
                var fecha = r.FechaCreacion.HasValue
                    ? r.FechaCreacion.Value.ToString("dd/MM/yyyy")
                    : "—";
                AddCell(table, fecha);

                // Inside ExportPdfITextSharp method:
                AddCell(
                    table,
                    r.MontoTotal?.ToString("C", CultureInfo.GetCultureInfo("es-DO")) ?? "—"
                );
            }


            doc.Add(table);
            doc.Close();

            // 7. Devolver archivo
            var pdfBytes = ms.ToArray();
            return File(pdfBytes, "application/pdf", "Reservas_Activas_iTextSharp.pdf");
        }

        // Helper para celdas
        private void AddCell(PdfPTable table, string text, bool header = false)
        {
            var font = FontFactory.GetFont(
                BaseFont.HELVETICA,
                12,
                header ? Font.BOLD : Font.NORMAL
            );
            var cell = new PdfPCell(new Phrase(text, font))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5,
                BackgroundColor = header ? BaseColor.LIGHT_GRAY : BaseColor.WHITE
            };
            table.AddCell(cell);
        }




    }
}
