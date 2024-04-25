using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurante.Models;

namespace restaurante.Controllers
{
    public class platoController : Controller
    {
        private readonly restauranteDbContext _context;

        public platoController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: plato
        public async Task<IActionResult> Index()
        {
            return View(await _context.plato.ToListAsync());
        }

        // Acción para cambiar el estado del plato
        [HttpPost]
        public IActionResult CambiarEstado(int id)
        {
            // Buscar el plato en la base de datos por su ID
            var plato = _context.plato.FirstOrDefault(p => p.id_Plato == id);

            if (plato == null)
            {
                // Si el plato no se encuentra, devolver un error o realizar alguna otra acción
                return NotFound();
            }


            //Guardar los cambios en la base de datos
            _context.SaveChanges();

            //Devolver una respuesta exitosa
            return Ok();
        }


        // GET: plato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.plato
                .FirstOrDefaultAsync(m => m.id_Plato == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // GET: plato/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: plato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_Plato,nombre,precio")] plato plato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plato);
        }

        // GET: plato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.plato.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            return View(plato);
        }

        // POST: plato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_Plato,nombre,precio")] plato plato)
        {
            if (id != plato.id_Plato)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!platoExists(plato.id_Plato))
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
            return View(plato);
        }

        // GET: plato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.plato
                .FirstOrDefaultAsync(m => m.id_Plato == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // POST: plato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plato = await _context.plato.FindAsync(id);
            if (plato != null)
            {
                _context.plato.Remove(plato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool platoExists(int id)
        {
            return _context.plato.Any(e => e.id_Plato == id);
        }




    }
}
