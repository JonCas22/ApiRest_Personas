using ApiRest_Personas.Models;
using Prueba.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiRest_Personas.Repository
{
    public class PersonaRepository : RepositoryBase<Personas>, IPersonaRepository
    {
        private readonly MasterContext _context;

        public PersonaRepository(MasterContext context) : base(context)
        {
            _context = context;
        }
    }
}
