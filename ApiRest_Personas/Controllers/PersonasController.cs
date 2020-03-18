using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRest_Personas.Models;
using Prueba.Models;

namespace ApiRest_Personas.Controllers
{
    [Route("/personas")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly masterContext _context;

        public PersonasController(masterContext context)
        {
            _context = context;
        }

        // GET: api/Personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personas>>> GetPersonas()
        {
            return await _context.Personas.ToListAsync();
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personas>> GetPersonas(long id)
        {
            var personas = await _context.Personas.FindAsync(id);

            if (personas == null)
            {
                return NotFound();
            }

            return personas;
        }

        // PUT: api/Personas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Personas>>> PutPersonas(Personas personas)
        {

            _context.Entry(personas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                 throw;
            }

            return await _context.Personas.ToListAsync();
        }

        // POST: api/Personas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Personas>>> PostPersonas(Personas personas)
        {
            _context.Personas.Add(personas);
            await _context.SaveChangesAsync();

            return await _context.Personas.ToListAsync();
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Personas>>> DeletePersonas(long id)
        {
            var personas = await _context.Personas.FindAsync(id);
            if (personas == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(personas);
            await _context.SaveChangesAsync();

            return await _context.Personas.ToListAsync();
        }

        private bool PersonasExists(long id)
        {
            return _context.Personas.Any(e => e.Id == id);
        }
    }
}
