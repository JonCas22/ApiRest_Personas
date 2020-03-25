using ApiRest_Personas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest_Personas.Application
{
    public interface IPersonaService
    {
        Task<IEnumerable<Personas>> GetAllPersonas();

        Task<Personas> GetPersonaById(long id);

        Task<Personas> PutPersona(long id, Personas personas);

        Task<Personas> PostPersona(Personas personas);

        Task<Personas> DeletePersona(long id);

        bool PersonasExists(long id);
    }
}
