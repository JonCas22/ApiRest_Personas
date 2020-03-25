using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRest_Personas.Models;
using Microsoft.AspNetCore.Authorization;
using ApiRest_Personas.Application;

namespace ApiRest_Personas.Controllers
{
    [Authorize]
    [Route("/users")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class UsersController : ControllerBase
    {
        //private readonly MasterContext _context;
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _userService.GetAllUsers();
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
            var users = await _userService.GetUserById(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(long id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            Console.WriteLine("The id is" + id);

            try
            {
                await _userService.PutUser(id, users);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            await _userService.PostUser(users);

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(long id)
        {
            var users = await _userService.DeleteUser(id);
            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        private bool UsersExists(long id)
        {
            return _userService.UsersExists(id);
        }
    }
}
