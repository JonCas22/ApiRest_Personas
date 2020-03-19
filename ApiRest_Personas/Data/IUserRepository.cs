using ApiRest_Personas.Models;
using System.Threading.Tasks;

namespace ApiRest_Personas.Data
{
    public interface IUserRepository
    {
        Task<Users> AutenticarUsuarioAsync(string usuario, string password);
    }
}
