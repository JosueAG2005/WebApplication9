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
    public class TiposDeConcursoesController : Controller
    {
        private readonly SistemaDeConcursosContext _context;

        public TiposDeConcursoesController(SistemaDeConcursosContext context)
        {
            _context = context;
        }

        // GET: TiposDeConcursoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposDeConcursos.ToListAsync());
        }

        // GET: TiposDeConcursoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposDeConcurso = await _context.TiposDeConcursos
                .FirstOrDefaultAsync(m => m.IdTipoConcurso == id);
            if (tiposDeConcurso == null)
            {
                return NotFound();
            }

            return View(tiposDeConcurso);
        }

        // GET: TiposDeConcursoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposDeConcursoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoConcurso,NombreTipo,Descripcion")] TiposDeConcurso tiposDeConcurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposDeConcurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposDeConcurso);
        }

        // GET: TiposDeConcursoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposDeConcurso = await _context.TiposDeConcursos.FindAsync(id);
            if (tiposDeConcurso == null)
            {
                return NotFound();
            }
            return View(tiposDeConcurso);
        }

        // POST: TiposDeConcursoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoConcurso,NombreTipo,Descripcion")] TiposDeConcurso tiposDeConcurso)
        {
            if (id != tiposDeConcurso.IdTipoConcurso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposDeConcurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposDeConcursoExists(tiposDeConcurso.IdTipoConcurso))
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
            return View(tiposDeConcurso);
        }

        // GET: TiposDeConcursoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposDeConcurso = await _context.TiposDeConcursos
                .FirstOrDefaultAsync(m => m.IdTipoConcurso == id);
            if (tiposDeConcurso == null)
            {
                return NotFound();
            }

            return View(tiposDeConcurso);
        }

        // POST: TiposDeConcursoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposDeConcurso = await _context.TiposDeConcursos.FindAsync(id);
            if (tiposDeConcurso != null)
            {
                _context.TiposDeConcursos.Remove(tiposDeConcurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposDeConcursoExists(int id)
        {
            return _context.TiposDeConcursos.Any(e => e.IdTipoConcurso == id);
        }
    }
}
