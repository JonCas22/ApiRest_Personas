using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRest_Personas.Models;
using Prueba.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiRest_Personas.Controllers
{
    [Authorize]
    [Route("/personas")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PersonasController : ControllerBase
    {
        private readonly masterContext _context;

        public PersonasController(masterContext context)
        {
            _context = context;
        }

        // GET: api/Personas1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personas>>> GetPersonas()
        {
            return await _context.Personas.ToListAsync();
        }

        // GET: api/Personas1/5
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

        // PUT: api/Personas1/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonas(long id, Personas personas)
        {
            if (id != personas.Id)
            {
                return BadRequest();
            }

            _context.Entry(personas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Personas1
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Personas>> PostPersonas(Personas personas)
        {
            _context.Personas.Add(personas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonas", new { id = personas.Id }, personas);
        }

        // DELETE: api/Personas1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personas>> DeletePersonas(long id)
        {
            var personas = await _context.Personas.FindAsync(id);
            if (personas == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(personas);
            await _context.SaveChangesAsync();

            return personas;
        }

        private bool PersonasExists(long id)
        {
            return _context.Personas.Any(e => e.Id == id);
        }
    }
}
