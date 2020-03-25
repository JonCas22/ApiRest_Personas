using ApiRest_Personas.Models;
using Microsoft.EntityFrameworkCore;
using Prueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest_Personas.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly MasterContext _context;

        public UserRepository(MasterContext context)
        {
            _context = context;
        }


        public async Task<Users> AutenticarUsuarioAsync(string usuario, string password)
        {
            var search = await _context.Users.FirstOrDefaultAsync(u => u.Username == usuario && u.Password == password);
            Console.WriteLine("Search=> " + search);
            if (search != null)
            {
                Console.WriteLine(search);

            }
            else
            {
                Console.WriteLine("Usuario no encontrado");
            }

            if (usuario != null) { Console.WriteLine(usuario); } else { Console.WriteLine("Nombre no encontrado"); }
            if (password != null) { Console.WriteLine(password); } else { Console.WriteLine("Password no encontrado"); }

            return search;
        }
    }
}
