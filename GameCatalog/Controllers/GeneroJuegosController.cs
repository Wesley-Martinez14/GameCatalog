using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameCatalog.Data;
using GameCatalog.Models;
using Microsoft.AspNetCore.Authorization;

namespace GameCatalog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GeneroJuegosController : Controller
    {
        private readonly GameCatalogContext _context;

        public GeneroJuegosController(GameCatalogContext context)
        {
            _context = context;
        }

        // GET: GeneroJuegos
        public async Task<IActionResult> Index()
        {
              return _context.GeneroJuegos != null ? 
                          View(await _context.GeneroJuegos.ToListAsync()) :
                          Problem("Entity set 'GameCatalogContext.GeneroJuegos'  is null.");
        }

        // GET: GeneroJuegos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GeneroJuegos == null)
            {
                return NotFound();
            }

            var generoJuego = await _context.GeneroJuegos
                .FirstOrDefaultAsync(m => m.GeneroId == id);
            if (generoJuego == null)
            {
                return NotFound();
            }

            return View(generoJuego);
        }

        // GET: GeneroJuegos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneroJuegos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GeneroId,NombreGenero,DescripcionGenero")] GeneroJuego generoJuego)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generoJuego);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(generoJuego);
        }

        // GET: GeneroJuegos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GeneroJuegos == null)
            {
                return NotFound();
            }

            var generoJuego = await _context.GeneroJuegos.FindAsync(id);
            if (generoJuego == null)
            {
                return NotFound();
            }
            return View(generoJuego);
        }

        // POST: GeneroJuegos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GeneroId,NombreGenero,DescripcionGenero")] GeneroJuego generoJuego)
        {
            if (id != generoJuego.GeneroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generoJuego);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneroJuegoExists(generoJuego.GeneroId))
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
            return View(generoJuego);
        }

        // GET: GeneroJuegos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GeneroJuegos == null)
            {
                return NotFound();
            }

            var generoJuego = await _context.GeneroJuegos
                .FirstOrDefaultAsync(m => m.GeneroId == id);
            if (generoJuego == null)
            {
                return NotFound();
            }

            return View(generoJuego);
        }

        // POST: GeneroJuegos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GeneroJuegos == null)
            {
                return Problem("Entity set 'GameCatalogContext.GeneroJuegos'  is null.");
            }
            var generoJuego = await _context.GeneroJuegos.FindAsync(id);
            if (generoJuego != null)
            {
                _context.GeneroJuegos.Remove(generoJuego);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneroJuegoExists(int id)
        {
          return (_context.GeneroJuegos?.Any(e => e.GeneroId == id)).GetValueOrDefault();
        }
    }
}
