using ApiRest_Personas.Data;
using ApiRest_Personas.Models;
using ApiRest_Personas.Repository;
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
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await _userRepository.FindAll();
        }

        public async Task<Users> GetUserById(long id)
        {
            var user = await _userRepository.GetById(id);

            return user.Result;
        }

        public async Task<Users> PostUser(Users users)
        {
            await _userRepository.Create(users);

            return await (_userRepository.GetById(users.Id)).Result;
        }

        public async Task<Users> PutUser(long id, Users users)
        {
            await _userRepository.Update(users);
            return await (_userRepository.GetById(users.Id)).Result;
        }

        public async Task<Users> DeleteUser(long id)
        {
            var user = await (_userRepository.GetById(id)).Result;

            await _userRepository.Delete(user);

            return user;
        }

        public bool UsersExists(long id)
        {
            //return _context.Users.Any(e => e.Id == id);
            return false;
        }
    }
}
