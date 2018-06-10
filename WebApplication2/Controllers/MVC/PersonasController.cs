using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entidades.Negocio;
using WebApplication2.Data;
using Entidades.ViewModels;

namespace WebApplication2.Controllers.MVC
{
    public class PersonasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            var person = await _context.Persona.Select(x => new ViewModelPersonaGenero
            {
                IdPersona = x.IdPersona,
                Cedula = x.Cedula,
                Nombre=x.Nombre,
                Apellido =x.Apellido,
                Direccion = x.Direccion,
                DescripcionGenero = x.IdGeneroNavigation.Descripcion
            }).ToListAsync();
            //var applicationDbContext = _context.Persona.Include(p => p.IdGeneroNavigation);
            //return View(await applicationDbContext.ToListAsync());
            return View(person);
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = await _context.Persona.Where(m => m.IdPersona == id).Select(x => new ViewModelPersonaGenero
            {
                IdPersona = x.IdPersona,
                Cedula = x.Cedula,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Direccion = x.Direccion,
                DescripcionGenero = x.IdGeneroNavigation.Descripcion
            }).FirstOrDefaultAsync();
            //var persona = await _context.Persona
            //    .Include(p => p.IdGeneroNavigation)
            //    .SingleOrDefaultAsync(m => m.IdPersona == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            ViewData["IdGenero"] = new SelectList(_context.Genero, "IdGenero", "Descripcion");
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPersona,Cedula,Nombre,Apellido,Direccion,IdGenero")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGenero"] = new SelectList(_context.Genero, "IdGenero", "Descripcion", persona.IdGenero);
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona.SingleOrDefaultAsync(m => m.IdPersona == id);
            if (persona == null)
            {
                return NotFound();
            }
            ViewData["IdGenero"] = new SelectList(_context.Genero, "IdGenero", "Descripcion", persona.IdGenero);
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPersona,Cedula,Nombre,Apellido,Direccion,IdGenero")] Persona persona)
        {
            if (id != persona.IdPersona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.IdPersona))
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
            ViewData["IdGenero"] = new SelectList(_context.Genero, "IdGenero", "Descripcion", persona.IdGenero);
            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .Include(p => p.IdGeneroNavigation)
                .SingleOrDefaultAsync(m => m.IdPersona == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _context.Persona.SingleOrDefaultAsync(m => m.IdPersona == id);
            _context.Persona.Remove(persona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.IdPersona == id);
        }
    }
}
