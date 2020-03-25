using ApiRest_Personas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest_Personas.Application
{
    public class UserService : ControllerBase, IUserService
    {
        private readonly MasterContext _context;

        public UserService(MasterContext masterContext)
        {
            _context = masterContext;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetUserById(long id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        public async Task<Users> PostUser(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return await _context.Users.FindAsync(users.Id);
        }

        public async Task<Users> PutUser(long id, Users users)
        {

            Console.WriteLine("The id is" + id);

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                var user = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return await _context.Users.FindAsync(id);
        }

        public async Task<Users> DeleteUser(long id)
        {
            var users = await _context.Users.FindAsync(id);

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        public bool UsersExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
