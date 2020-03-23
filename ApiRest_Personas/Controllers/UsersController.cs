using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRest_Personas.Models;
using Prueba.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiRest_Personas.Controllers
{
    [Authorize]
    [Route("/users")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class UsersController : ControllerBase
    {
        private readonly masterContext _context;

        public UsersController(masterContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet]
        [MapToApiVersion("1.1")] // v1.1 specific action for GET api/values endpoint
        public ActionResult<IEnumerable<string>> GetV1_1()
        {
            return new string[] { "version 1.1 value 1", "version 1.1 value2 " };
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(long id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Users>>> PutUsers(Users users)
        {

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
                
            }

            return await _context.Users.ToListAsync();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Users>>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return await _context.Users.ToListAsync();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Users>>> DeleteUsers(long id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return await _context.Users.ToListAsync(); ;
        }

        private bool UsersExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
