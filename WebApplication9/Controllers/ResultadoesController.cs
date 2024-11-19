using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class ResultadoesController : Controller
    {
        private readonly SistemaDeConcursosContext _context;

        public ResultadoesController(SistemaDeConcursosContext context)
        {
            _context = context;
        }

        // GET: Resultadoes
        public async Task<IActionResult> Index()
        {
            var sistemaDeConcursosContext = _context.Resultados.Include(r => r.IdInscripcionNavigation);
            return View(await sistemaDeConcursosContext.ToListAsync());
        }

        // GET: Resultadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultados
                .Include(r => r.IdInscripcionNavigation)
                .FirstOrDefaultAsync(m => m.IdResultado == id);
            if (resultado == null)
            {
                return NotFound();
            }

            return View(resultado);
        }

        // GET: Resultadoes/Create
        public IActionResult Create()
        {
            ViewData["IdInscripcion"] = new SelectList(_context.Inscripciones, "IdInscripcion", "IdInscripcion");
            return View();
        }

        // POST: Resultadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdResultado,IdInscripcion,PuntuacionFinal,Clasificacion")] Resultado resultado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInscripcion"] = new SelectList(_context.Inscripciones, "IdInscripcion", "IdInscripcion", resultado.IdInscripcion);
            return View(resultado);
        }

        // GET: Resultadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultados.FindAsync(id);
            if (resultado == null)
            {
                return NotFound();
            }
            ViewData["IdInscripcion"] = new SelectList(_context.Inscripciones, "IdInscripcion", "IdInscripcion", resultado.IdInscripcion);
            return View(resultado);
        }

        // POST: Resultadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdResultado,IdInscripcion,PuntuacionFinal,Clasificacion")] Resultado resultado)
        {
            if (id != resultado.IdResultado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resultado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultadoExists(resultado.IdResultado))
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
            ViewData["IdInscripcion"] = new SelectList(_context.Inscripciones, "IdInscripcion", "IdInscripcion", resultado.IdInscripcion);
            return View(resultado);
        }

        // GET: Resultadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultados
                .Include(r => r.IdInscripcionNavigation)
                .FirstOrDefaultAsync(m => m.IdResultado == id);
            if (resultado == null)
            {
                return NotFound();
            }

            return View(resultado);
        }

        // POST: Resultadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultado = await _context.Resultados.FindAsync(id);
            if (resultado != null)
            {
                _context.Resultados.Remove(resultado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultadoExists(int id)
        {
            return _context.Resultados.Any(e => e.IdResultado == id);
        }
    }
}
