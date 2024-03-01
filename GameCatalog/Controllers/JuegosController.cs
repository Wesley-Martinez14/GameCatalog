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
    public class JuegosController : Controller
    {
        private readonly GameCatalogContext _context;

        public JuegosController(GameCatalogContext context)
        {
            _context = context;
        }

        // GET: Juegos
        public async Task<IActionResult> Index()
        {
            var gameCatalogContext = _context.Juegos.Include(j => j.ClasificacionJuego).Include(j => j.DisponibilidadJuego).Include(j => j.EmpresaJuego).Include(j => j.GeneroJuego);
            return View(await gameCatalogContext.ToListAsync());
        }

        // GET: Juegos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Juegos == null)
            {
                return NotFound();
            }

            var juego = await _context.Juegos
                .Include(j => j.ClasificacionJuego)
                .Include(j => j.DisponibilidadJuego)
                .Include(j => j.EmpresaJuego)
                .Include(j => j.GeneroJuego)
                .FirstOrDefaultAsync(m => m.JuegoId == id);
            if (juego == null)
            {
                return NotFound();
            }

            return View(juego);
        }

        // GET: Juegos/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ClasificacionJuegoId"] = new SelectList(_context.ClasificacionJuegos, "ClasificacionId", "NombreClasificacion");
            ViewData["DisponibilidadJuegoId"] = new SelectList(_context.Disponibilidads, "DisponibilidadId", "NombreDisponibilidad");
            ViewData["EmpresaJuegoId"] = new SelectList(_context.Empresas, "EmpresaId", "NombreEmpresa");
            ViewData["GeneroJuegoId"] = new SelectList(_context.GeneroJuegos, "GeneroId", "NombreGenero");
            return View();
        }

        // POST: Juegos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JuegoId,Nombre,Descripcion,GeneroJuegoId,ClasificacionJuegoId,EmpresaJuegoId,DisponibilidadJuegoId,FechaEstreno")] Juego juego)
        {
            if (ModelState.IsValid)
            {
                _context.Add(juego);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasificacionJuegoId"] = new SelectList(_context.ClasificacionJuegos, "ClasificacionId", "NombreClasificacion", juego.ClasificacionJuegoId);
            ViewData["DisponibilidadJuegoId"] = new SelectList(_context.Disponibilidads, "DisponibilidadId", "NombreDisponibilidad", juego.DisponibilidadJuegoId);
            ViewData["EmpresaJuegoId"] = new SelectList(_context.Empresas, "EmpresaId", "NombreEmpresa", juego.EmpresaJuegoId);
            ViewData["GeneroJuegoId"] = new SelectList(_context.GeneroJuegos, "GeneroId", "NombreGenero", juego.GeneroJuegoId);
            return View(juego);
        }

        // GET: Juegos/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Juegos == null)
            {
                return NotFound();
            }

            var juego = await _context.Juegos.FindAsync(id);
            if (juego == null)
            {
                return NotFound();
            }
            ViewData["ClasificacionJuegoId"] = new SelectList(_context.ClasificacionJuegos, "ClasificacionId", "NombreClasificacion", juego.ClasificacionJuegoId);
            ViewData["DisponibilidadJuegoId"] = new SelectList(_context.Disponibilidads, "DisponibilidadId", "NombreDisponibilidad", juego.DisponibilidadJuegoId);
            ViewData["EmpresaJuegoId"] = new SelectList(_context.Empresas, "EmpresaId", "NombreEmpresa", juego.EmpresaJuegoId);
            ViewData["GeneroJuegoId"] = new SelectList(_context.GeneroJuegos, "GeneroId", "NombreGenero", juego.GeneroJuegoId);
            return View(juego);
        }

        // POST: Juegos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JuegoId,Nombre,Descripcion,GeneroJuegoId,ClasificacionJuegoId,EmpresaJuegoId,DisponibilidadJuegoId,FechaEstreno")] Juego juego)
        {
            if (id != juego.JuegoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(juego);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JuegoExists(juego.JuegoId))
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
            ViewData["ClasificacionJuegoId"] = new SelectList(_context.ClasificacionJuegos, "ClasificacionId", "NombreClasificacion", juego.ClasificacionJuegoId);
            ViewData["DisponibilidadJuegoId"] = new SelectList(_context.Disponibilidads, "DisponibilidadId", "NombreDisponibilidad", juego.DisponibilidadJuegoId);
            ViewData["EmpresaJuegoId"] = new SelectList(_context.Empresas, "EmpresaId", "NombreEmpresa", juego.EmpresaJuegoId);
            ViewData["GeneroJuegoId"] = new SelectList(_context.GeneroJuegos, "GeneroId", "NombreGenero", juego.GeneroJuegoId);
            return View(juego);
        }

        // GET: Juegos/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Juegos == null)
            {
                return NotFound();
            }

            var juego = await _context.Juegos
                .Include(j => j.ClasificacionJuego)
                .Include(j => j.DisponibilidadJuego)
                .Include(j => j.EmpresaJuego)
                .Include(j => j.GeneroJuego)
                .FirstOrDefaultAsync(m => m.JuegoId == id);
            if (juego == null)
            {
                return NotFound();
            }

            return View(juego);
        }

        // POST: Juegos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Juegos == null)
            {
                return Problem("Entity set 'GameCatalogContext.Juegos'  is null.");
            }
            var juego = await _context.Juegos.FindAsync(id);
            if (juego != null)
            {
                _context.Juegos.Remove(juego);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JuegoExists(int id)
        {
          return (_context.Juegos?.Any(e => e.JuegoId == id)).GetValueOrDefault();
        }
    }
}
