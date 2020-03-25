using ApiRest_Personas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRest_Personas.Application
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsers();

        Task<Users> GetUserById(long id);

        Task<Users> PutUser(long id, Users users);

        Task<Users> PostUser(Users users);

        Task<Users> DeleteUser(long id);

        bool UsersExists(long id);
    }
}
