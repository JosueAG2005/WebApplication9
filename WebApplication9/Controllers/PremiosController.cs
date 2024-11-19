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
    public class PremiosController : Controller
    {
        private readonly SistemaDeConcursosContext _context;

        public PremiosController(SistemaDeConcursosContext context)
        {
            _context = context;
        }

        // GET: Premios
        public async Task<IActionResult> Index()
        {
            var sistemaDeConcursosContext = _context.Premios.Include(p => p.IdConcursoNavigation);
            return View(await sistemaDeConcursosContext.ToListAsync());
        }

        // GET: Premios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premio = await _context.Premios
                .Include(p => p.IdConcursoNavigation)
                .FirstOrDefaultAsync(m => m.IdPremio == id);
            if (premio == null)
            {
                return NotFound();
            }

            return View(premio);
        }

        // GET: Premios/Create
        public IActionResult Create()
        {
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "IdConcurso");
            return View();
        }

        // POST: Premios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPremio,IdConcurso,Descripcion,Cantidad")] Premio premio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(premio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "IdConcurso", premio.IdConcurso);
            return View(premio);
        }

        // GET: Premios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premio = await _context.Premios.FindAsync(id);
            if (premio == null)
            {
                return NotFound();
            }
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "IdConcurso", premio.IdConcurso);
            return View(premio);
        }

        // POST: Premios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPremio,IdConcurso,Descripcion,Cantidad")] Premio premio)
        {
            if (id != premio.IdPremio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(premio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PremioExists(premio.IdPremio))
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
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "IdConcurso", premio.IdConcurso);
            return View(premio);
        }

        // GET: Premios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premio = await _context.Premios
                .Include(p => p.IdConcursoNavigation)
                .FirstOrDefaultAsync(m => m.IdPremio == id);
            if (premio == null)
            {
                return NotFound();
            }

            return View(premio);
        }

        // POST: Premios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var premio = await _context.Premios.FindAsync(id);
            if (premio != null)
            {
                _context.Premios.Remove(premio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PremioExists(int id)
        {
            return _context.Premios.Any(e => e.IdPremio == id);
        }
    }
}
