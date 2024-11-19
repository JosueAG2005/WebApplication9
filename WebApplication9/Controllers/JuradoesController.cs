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
    public class JuradoesController : Controller
    {
        private readonly SistemaDeConcursosContext _context;

        public JuradoesController(SistemaDeConcursosContext context)
        {
            _context = context;
        }

        // GET: Juradoes
        public async Task<IActionResult> Index()
        {
            var sistemaDeConcursosContext = _context.Jurados.Include(j => j.IdConcursoNavigation).Include(j => j.IdUsuarioNavigation);
            return View(await sistemaDeConcursosContext.ToListAsync());
        }

        // GET: Juradoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jurado = await _context.Jurados
                .Include(j => j.IdConcursoNavigation)
                .Include(j => j.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdJurado == id);
            if (jurado == null)
            {
                return NotFound();
            }

            return View(jurado);
        }

        // GET: Juradoes/Create
        public IActionResult Create()
        {
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "IdConcurso");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Juradoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdJurado,IdUsuario,IdConcurso")] Jurado jurado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jurado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "IdConcurso", jurado.IdConcurso);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", jurado.IdUsuario);
            return View(jurado);
        }

        // GET: Juradoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jurado = await _context.Jurados.FindAsync(id);
            if (jurado == null)
            {
                return NotFound();
            }
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "IdConcurso", jurado.IdConcurso);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", jurado.IdUsuario);
            return View(jurado);
        }

        // POST: Juradoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdJurado,IdUsuario,IdConcurso")] Jurado jurado)
        {
            if (id != jurado.IdJurado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jurado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JuradoExists(jurado.IdJurado))
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
            ViewData["IdConcurso"] = new SelectList(_context.Concursos, "IdConcurso", "IdConcurso", jurado.IdConcurso);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", jurado.IdUsuario);
            return View(jurado);
        }

        // GET: Juradoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jurado = await _context.Jurados
                .Include(j => j.IdConcursoNavigation)
                .Include(j => j.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdJurado == id);
            if (jurado == null)
            {
                return NotFound();
            }

            return View(jurado);
        }

        // POST: Juradoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jurado = await _context.Jurados.FindAsync(id);
            if (jurado != null)
            {
                _context.Jurados.Remove(jurado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JuradoExists(int id)
        {
            return _context.Jurados.Any(e => e.IdJurado == id);
        }
    }
}
