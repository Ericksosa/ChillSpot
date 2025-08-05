using ChillSpot.Data;
using ChillSpot.Filters;
using ChillSpot.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChillSpot.Areas.Administrador.Controllers
{
    [SessionAuthorize("1")]
    [Area("Administrador")]
    public class IdiomasController : Controller
    {
        private readonly chillSpotDbContext _context;

        public IdiomasController(chillSpotDbContext context)
        {
            _context = context;
        }

        // GET: Idiomas
        public async Task<IActionResult> Index()
        {
            var chillSpotDbContext = _context.Idiomas.Include(i => i.Estado);
            return View(await chillSpotDbContext.ToListAsync());
        }

        // GET: Idiomas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idioma = await _context.Idiomas
                .Include(i => i.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idioma == null)
            {
                return NotFound();
            }

            return View(idioma);
        }

        // GET: Idiomas/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre");
            return View();
        }

        // POST: Idiomas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,EstadoId")] Idioma idioma)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Add(idioma);
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

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", idioma.EstadoId);

            return View(idioma);
        }

        // GET: Idiomas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idioma = await _context.Idiomas.FindAsync(id);
            if (idioma == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", idioma.EstadoId);
            return View(idioma);
        }

        // POST: Idiomas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Descripcion,EstadoId")] Idioma idioma)
        {
            if (id != idioma.Id)
            {
                return NotFound();
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Update(idioma);
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

            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Nombre", idioma.EstadoId);

            return View(idioma);
        }

        // GET: Idiomas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idioma = await _context.Idiomas
                .Include(i => i.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idioma == null)
            {
                return NotFound();
            }

            return View(idioma);
        }

        // POST: Idiomas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var idioma = await _context.Idiomas.FindAsync(id);
                    if (idioma != null)
                    {
                        _context.Idiomas.Remove(idioma);
                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync(); // Confirma eliminación si todo va bien
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(); // Revierte cambios en caso de error

                    ModelState.AddModelError("", "No se pudo eliminar el idioma. Inténtalo nuevamente.");
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool IdiomaExists(long id)
        {
            return _context.Idiomas.Any(e => e.Id == id);
        }

        // GET: Idiomas/ExportPdfITextSharp
        public async Task<IActionResult> ExportPdfITextSharp()
        {
            // 1. Obtener todos los idiomas con su estado
            var idiomas = await _context.Idiomas
                .Include(i => i.Estado)
                .ToListAsync();

            // 2. Crear el documento en memoria
            using var ms = new MemoryStream();
            var doc = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter.GetInstance(doc, ms);
            doc.Open();

            // 3. Título
            var titleFont = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 16);
            var title = new Paragraph("Listado de Idiomas", titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20
            };
            doc.Add(title);

            // 4. Tabla con columnas: ID, Nombre, Descripción, Estado
            var table = new PdfPTable(4) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 1f, 3f, 5f, 2f });

            // 5. Encabezados
            AddCell(table, "ID", true);
            AddCell(table, "Nombre", true);
            AddCell(table, "Descripción", true);
            AddCell(table, "Estado", true);

            // 6. Filas de datos
            foreach (var i in idiomas)
            {
                AddCell(table, i.Id.ToString());
                AddCell(table, i.Nombre ?? "—");
                AddCell(table, i.Descripcion ?? "—");
                AddCell(table, i.Estado?.Nombre ?? "—");
            }

            doc.Add(table);
            doc.Close();

            // 7. Enviar el PDF al cliente
            var bytes = ms.ToArray();
            return File(bytes, "application/pdf", "Idiomas.pdf");
        }

        // Helper para añadir celdas a la tabla
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
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 5,
                BackgroundColor = header ? BaseColor.LIGHT_GRAY : BaseColor.WHITE
            };
            table.AddCell(cell);
        }
    }
}
