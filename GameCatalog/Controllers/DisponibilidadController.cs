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
    public class DisponibilidadController : Controller
    {
        private readonly GameCatalogContext _context;

        public DisponibilidadController(GameCatalogContext context)
        {
            _context = context;
        }

        // GET: Disponibilidad
        public async Task<IActionResult> Index()
        {
              return _context.Disponibilidads != null ? 
                          View(await _context.Disponibilidads.ToListAsync()) :
                          Problem("Entity set 'GameCatalogContext.Disponibilidads'  is null.");
        }

        // GET: Disponibilidad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Disponibilidads == null)
            {
                return NotFound();
            }

            var disponibilidad = await _context.Disponibilidads
                .FirstOrDefaultAsync(m => m.DisponibilidadId == id);
            if (disponibilidad == null)
            {
                return NotFound();
            }

            return View(disponibilidad);
        }

        // GET: Disponibilidad/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disponibilidad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisponibilidadId,NombreDisponibilidad")] Disponibilidad disponibilidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disponibilidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disponibilidad);
        }

        // GET: Disponibilidad/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Disponibilidads == null)
            {
                return NotFound();
            }

            var disponibilidad = await _context.Disponibilidads.FindAsync(id);
            if (disponibilidad == null)
            {
                return NotFound();
            }
            return View(disponibilidad);
        }

        // POST: Disponibilidad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisponibilidadId,NombreDisponibilidad")] Disponibilidad disponibilidad)
        {
            if (id != disponibilidad.DisponibilidadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disponibilidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisponibilidadExists(disponibilidad.DisponibilidadId))
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
            return View(disponibilidad);
        }

        // GET: Disponibilidad/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Disponibilidads == null)
            {
                return NotFound();
            }

            var disponibilidad = await _context.Disponibilidads
                .FirstOrDefaultAsync(m => m.DisponibilidadId == id);
            if (disponibilidad == null)
            {
                return NotFound();
            }

            return View(disponibilidad);
        }

        // POST: Disponibilidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Disponibilidads == null)
            {
                return Problem("Entity set 'GameCatalogContext.Disponibilidads'  is null.");
            }
            var disponibilidad = await _context.Disponibilidads.FindAsync(id);
            if (disponibilidad != null)
            {
                _context.Disponibilidads.Remove(disponibilidad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisponibilidadExists(int id)
        {
          return (_context.Disponibilidads?.Any(e => e.DisponibilidadId == id)).GetValueOrDefault();
        }
    }
}
