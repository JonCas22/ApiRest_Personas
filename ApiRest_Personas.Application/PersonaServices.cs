using ApiRest_Personas.Models;
using Microsoft.EntityFrameworkCore;
using Prueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest_Personas.Application
{
    public class PersonaServices : IPersonaService
    {
        private readonly MasterContext _context;

        public PersonaServices(MasterContext masterContext)
        {
            _context = masterContext;
        }

        public async Task<IEnumerable<Personas>> GetAllPersonas()
        {
            return await _context.Personas.ToListAsync();

        }

        public async Task<Personas> GetPersonaById(long id)
        {
            var persona = await _context.Personas.FindAsync(id);

            return persona;
        }

        public async Task<Personas> PostPersona(Personas personas)
        {
            _context.Personas.Add(personas);
            await _context.SaveChangesAsync();

            return await _context.Personas.FindAsync(personas.Id);
        }

        public async Task<Personas> PutPersona(long id, Personas personas)
        {
            Console.WriteLine("The id is" + id);

            _context.Entry(personas).State = EntityState.Modified;

            try
            {
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return await _context.Personas.FindAsync(id);
        }

        public async Task<Personas> DeletePersona(long id)
        {
            var persona = await _context.Personas.FindAsync(id);

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return persona;
        }

        public bool PersonasExists(long id)
        {
            return _context.Personas.Any(e => e.Id == id);
        }
    }
}
