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
    public class PuntuacionesJuradoesController : Controller
    {
        private readonly SistemaDeConcursosContext _context;

        public PuntuacionesJuradoesController(SistemaDeConcursosContext context)
        {
            _context = context;
        }

        // GET: PuntuacionesJuradoes
        public async Task<IActionResult> Index()
        {
            var sistemaDeConcursosContext = _context.PuntuacionesJurados.Include(p => p.IdConcursoNavigation).Include(p => p.IdInscripcionNavigation).Include(p => p.IdJuradoNavigation);
            return View(await sistemaDeConcursosContext.ToListAsync());
        }

        // GET: PuntuacionesJuradoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puntuacionesJurado = await _context.PuntuacionesJurados
                .Include(p => p.IdConcursoNavigation)
                .Include(p => p.IdInscripcionNavigation)
                .Include(p => p.IdJuradoNavigation)
                .FirstOrDefaultAsync(m => m.IdPuntuacion == id);
            if (puntuacionesJurado == null)
            {
                return NotFound();
            }

            return View(puntuacionesJurado);
        }

        // GET: PuntuacionesJuradoes/Create
        public IActionResult Create()
        {
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "Nombre");
            ViewData["IdInscripcion"] = new SelectList(_context.Inscripciones, "IdInscripcion", "IdInscripcion");
            ViewData["IdJurado"] = new SelectList(_context.Jurados, "IdJurado", "IdJurado");
            return View();
        }

        // POST: PuntuacionesJuradoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPuntuacion,IdJurado,IdConcurso,IdInscripcion,Puntuacion,Comentarios,Razon")] PuntuacionesJurado puntuacionesJurado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(puntuacionesJurado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "IdConcurso", puntuacionesJurado.IdConcurso);
            ViewData["IdInscripcion"] = new SelectList(_context.Inscripciones, "IdInscripcion", "IdInscripcion", puntuacionesJurado.IdInscripcion);
            ViewData["IdJurado"] = new SelectList(_context.Jurados, "IdJurado", "IdJurado", puntuacionesJurado.IdJurado);
            return View(puntuacionesJurado);
        }

        // GET: PuntuacionesJuradoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puntuacionesJurado = await _context.PuntuacionesJurados.FindAsync(id);
            if (puntuacionesJurado == null)
            {
                return NotFound();
            }
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "IdConcurso", puntuacionesJurado.IdConcurso);
            ViewData["IdInscripcion"] = new SelectList(_context.Inscripciones, "IdInscripcion", "IdInscripcion", puntuacionesJurado.IdInscripcion);
            ViewData["IdJurado"] = new SelectList(_context.Jurados, "IdJurado", "IdJurado", puntuacionesJurado.IdJurado);
            return View(puntuacionesJurado);
        }

        // POST: PuntuacionesJuradoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPuntuacion,IdJurado,IdConcurso,IdInscripcion,Puntuacion,Comentarios,Razon")] PuntuacionesJurado puntuacionesJurado)
        {
            if (id != puntuacionesJurado.IdPuntuacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puntuacionesJurado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuntuacionesJuradoExists(puntuacionesJurado.IdPuntuacion))
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
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "IdConcurso", puntuacionesJurado.IdConcurso);
            ViewData["IdInscripcion"] = new SelectList(_context.Inscripciones, "IdInscripcion", "IdInscripcion", puntuacionesJurado.IdInscripcion);
            ViewData["IdJurado"] = new SelectList(_context.Jurados, "IdJurado", "IdJurado", puntuacionesJurado.IdJurado);
            return View(puntuacionesJurado);
        }

        // GET: PuntuacionesJuradoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puntuacionesJurado = await _context.PuntuacionesJurados
                .Include(p => p.IdConcursoNavigation)
                .Include(p => p.IdInscripcionNavigation)
                .Include(p => p.IdJuradoNavigation)
                .FirstOrDefaultAsync(m => m.IdPuntuacion == id);
            if (puntuacionesJurado == null)
            {
                return NotFound();
            }

            return View(puntuacionesJurado);
        }

        // POST: PuntuacionesJuradoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var puntuacionesJurado = await _context.PuntuacionesJurados.FindAsync(id);
            if (puntuacionesJurado != null)
            {
                _context.PuntuacionesJurados.Remove(puntuacionesJurado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuntuacionesJuradoExists(int id)
        {
            return _context.PuntuacionesJurados.Any(e => e.IdPuntuacion == id);
        }
    }
}
