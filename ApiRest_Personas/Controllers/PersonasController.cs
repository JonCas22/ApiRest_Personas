using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRest_Personas.Models;
using Microsoft.AspNetCore.Authorization;
using ApiRest_Personas.Application;

namespace ApiRest_Personas.Controllers
{
    [Authorize]
    [Route("/personas")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PersonasController : ControllerBase
    {
        //private readonly MasterContext _context;
        private readonly IPersonaService _personaService;

        public PersonasController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        // GET: api/Personas1
        [HttpGet]
        public async Task<IEnumerable<Personas>> GetPersonas()
        {
            return await _personaService.GetAllPersonas();
        }

        // GET: api/Personas1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personas>> GetPersonas(long id)
        {
            var personas = await _personaService.GetPersonaById(id);

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


            try
            {
                await _personaService.PutPersona(id, personas);
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
            await _personaService.PostPersona(personas);

            return CreatedAtAction("GetPersonas", new { id = personas.Id }, personas);
        }

        // DELETE: api/Personas1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personas>> DeletePersonas(long id)
        {
            var personas = await _personaService.DeletePersona(id);
            if (personas == null)
            {
                return NotFound();
            }

            return personas;
        }

        private bool PersonasExists(long id)
        {
            return _personaService.PersonasExists(id);
        }
    }
}
