using ApiRest_Personas.Models;
using ApiRest_Personas.Repository;
using System.Threading.Tasks;

namespace ApiRest_Personas.Data
{
    public interface IUserRepository : IRepositoryBase<Users>
    {
        Task<Users> AutenticarUsuarioAsync(string usuario, string password);
    }
}
