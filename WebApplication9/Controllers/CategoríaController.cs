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
    public class CategoríaController : Controller
    {
        private readonly SistemaDeConcursosContext _context;

        public CategoríaController(SistemaDeConcursosContext context)
        {
            _context = context;
        }

        // GET: Categoría
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorías.ToListAsync());
        }

        // GET: Categoría/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoría = await _context.Categorías
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoría == null)
            {
                return NotFound();
            }

            return View(categoría);
        }

        // GET: Categoría/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoría/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoria,NombreCategoria,Descripcion")] Categoría categoría)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoría);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoría);
        }

        // GET: Categoría/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoría = await _context.Categorías.FindAsync(id);
            if (categoría == null)
            {
                return NotFound();
            }
            return View(categoría);
        }

        // POST: Categoría/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoria,NombreCategoria,Descripcion")] Categoría categoría)
        {
            if (id != categoría.IdCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoría);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoríaExists(categoría.IdCategoria))
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
            return View(categoría);
        }

        // GET: Categoría/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoría = await _context.Categorías
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoría == null)
            {
                return NotFound();
            }

            return View(categoría);
        }

        // POST: Categoría/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoría = await _context.Categorías.FindAsync(id);
            if (categoría != null)
            {
                _context.Categorías.Remove(categoría);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoríaExists(int id)
        {
            return _context.Categorías.Any(e => e.IdCategoria == id);
        }
    }
}
