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
    [Authorize]
    public class ClasificacionJuegoesController : Controller
    {
        private readonly GameCatalogContext _context;

        public ClasificacionJuegoesController(GameCatalogContext context)
        {
            _context = context;
        }

        // GET: ClasificacionJuegoes
        public async Task<IActionResult> Index()
        {
              return _context.ClasificacionJuegos != null ? 
                          View(await _context.ClasificacionJuegos.ToListAsync()) :
                          Problem("Entity set 'GameCatalogContext.ClasificacionJuegos'  is null.");
        }

        // GET: ClasificacionJuegoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClasificacionJuegos == null)
            {
                return NotFound();
            }

            var clasificacionJuego = await _context.ClasificacionJuegos
                .FirstOrDefaultAsync(m => m.ClasificacionId == id);
            if (clasificacionJuego == null)
            {
                return NotFound();
            }

            return View(clasificacionJuego);
        }

        // GET: ClasificacionJuegoes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClasificacionJuegoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClasificacionId,NombreClasificacion,DescripcionClasificacion")] ClasificacionJuego clasificacionJuego)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clasificacionJuego);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clasificacionJuego);
        }

        // GET: ClasificacionJuegoes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClasificacionJuegos == null)
            {
                return NotFound();
            }

            var clasificacionJuego = await _context.ClasificacionJuegos.FindAsync(id);
            if (clasificacionJuego == null)
            {
                return NotFound();
            }
            return View(clasificacionJuego);
        }

        // POST: ClasificacionJuegoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClasificacionId,NombreClasificacion,DescripcionClasificacion")] ClasificacionJuego clasificacionJuego)
        {
            if (id != clasificacionJuego.ClasificacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clasificacionJuego);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasificacionJuegoExists(clasificacionJuego.ClasificacionId))
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
            return View(clasificacionJuego);
        }

        // GET: ClasificacionJuegoes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClasificacionJuegos == null)
            {
                return NotFound();
            }

            var clasificacionJuego = await _context.ClasificacionJuegos
                .FirstOrDefaultAsync(m => m.ClasificacionId == id);
            if (clasificacionJuego == null)
            {
                return NotFound();
            }

            return View(clasificacionJuego);
        }

        // POST: ClasificacionJuegoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClasificacionJuegos == null)
            {
                return Problem("Entity set 'GameCatalogContext.ClasificacionJuegos'  is null.");
            }
            var clasificacionJuego = await _context.ClasificacionJuegos.FindAsync(id);
            if (clasificacionJuego != null)
            {
                _context.ClasificacionJuegos.Remove(clasificacionJuego);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasificacionJuegoExists(int id)
        {
          return (_context.ClasificacionJuegos?.Any(e => e.ClasificacionId == id)).GetValueOrDefault();
        }
    }
}
