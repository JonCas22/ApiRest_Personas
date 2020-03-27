using ApiRest_Personas.Data;
using ApiRest_Personas.Models;
using ApiRest_Personas.Repository;
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
        private IPersonaRepository _personaRepository;


        public PersonaServices(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<IEnumerable<Personas>> GetAllPersonas()
        {
            return await _personaRepository.FindAll();

        }

        public async Task<Personas> GetPersonaById(long id)
        {
            var persona = await _personaRepository.GetById(id);

            return persona.Result;
        }

        public async Task<Personas> PostPersona(Personas personas)
        {
            await _personaRepository.Create(personas);

            return await _personaRepository.GetById(personas.Id).Result;
        }

        public async Task<Personas> PutPersona(long id, Personas personas)
        {
            await _personaRepository.Update(personas);

            return await _personaRepository.GetById(id).Result;

        }

        public async Task<Personas> DeletePersona(long id)
        {
            var persona = await (_personaRepository.GetById(id)).Result;

            await _personaRepository.Delete(persona);

            return persona;
        }

        public bool PersonasExists(long id)
        {
            //return _context.Personas.Any(e => e.Id == id);
            return false;
        }
    }
}
