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
    public class ConcursoesController : Controller
    {
        private readonly SistemaDeConcursosContext _context;

        public ConcursoesController(SistemaDeConcursosContext context)
        {
            _context = context;
        }

        // GET: Concursoes
        public async Task<IActionResult> Index()
        {
            var sistemaDeConcursosContext = _context.Concursos.Include(c => c.IdCategoriaNavigation).Include(c => c.IdTipoConcursoNavigation);
            return View(await sistemaDeConcursosContext.ToListAsync());
        }

        // GET: Concursoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concurso = await _context.Concursos
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdTipoConcursoNavigation)
                .FirstOrDefaultAsync(m => m.IdConcurso == id);
            if (concurso == null)
            {
                return NotFound();
            }

            return View(concurso);
        }

        // GET: Concursoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categorías, "IdCategoria", "IdCategoria");
            ViewData["IdTipoConcurso"] = new SelectList(_context.TiposDeConcursos, "IdTipoConcurso", "IdTipoConcurso");
            return View();
        }

        // POST: Concursoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConcurso,Nombre,Descripcion,FechaInicio,FechaFin,Estado,IdCategoria,IdTipoConcurso")] Concurso concurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorías, "IdCategoria", "IdCategoria", concurso.IdCategoria);
            ViewData["IdTipoConcurso"] = new SelectList(_context.TiposDeConcursos, "IdTipoConcurso", "IdTipoConcurso", concurso.IdTipoConcurso);
            return View(concurso);
        }

        // GET: Concursoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concurso = await _context.Concursos.FindAsync(id);
            if (concurso == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorías, "IdCategoria", "IdCategoria", concurso.IdCategoria);
            ViewData["IdTipoConcurso"] = new SelectList(_context.TiposDeConcursos, "IdTipoConcurso", "IdTipoConcurso", concurso.IdTipoConcurso);
            return View(concurso);
        }

        // POST: Concursoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConcurso,Nombre,Descripcion,FechaInicio,FechaFin,Estado,IdCategoria,IdTipoConcurso")] Concurso concurso)
        {
            if (id != concurso.IdConcurso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcursoExists(concurso.IdConcurso))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categorías, "IdCategoria", "IdCategoria", concurso.IdCategoria);
            ViewData["IdTipoConcurso"] = new SelectList(_context.TiposDeConcursos, "IdTipoConcurso", "IdTipoConcurso", concurso.IdTipoConcurso);
            return View(concurso);
        }

        // GET: Concursoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concurso = await _context.Concursos
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdTipoConcursoNavigation)
                .FirstOrDefaultAsync(m => m.IdConcurso == id);
            if (concurso == null)
            {
                return NotFound();
            }

            return View(concurso);
        }

        // POST: Concursoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concurso = await _context.Concursos.FindAsync(id);
            if (concurso != null)
            {
                _context.Concursos.Remove(concurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcursoExists(int id)
        {
            return _context.Concursos.Any(e => e.IdConcurso == id);
        }
    }
}
