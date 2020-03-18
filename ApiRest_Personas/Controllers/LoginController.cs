using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiRest_Personas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Prueba.Models;

namespace ApiRest_Personas.Controllers
{
    [Route("/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;

        private List<Users> users = new List<Users>{};
        private readonly masterContext _context;

        public LoginController(IConfiguration configuration, masterContext context)
        {
            this.configuration = configuration;
            _context = context;
        }

        // POST: api/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin usuarioLogin)
        {
            Console.WriteLine("Usuario Login es=>" + usuarioLogin.Username + " con contraseña=>" + usuarioLogin.Password);
            var _userInfo = await AutenticarUsuarioAsync(usuarioLogin.Username, usuarioLogin.Password);
            if (_userInfo != null)
            {
                return Ok(new { token = GenerarTokenJWT(_userInfo) });
            }
            else
            {
                return Unauthorized();
            }
        }

        // COMPROBAMOS SI EL USUARIO EXISTE EN LA BASE DE DATOS 
        private async Task<Users> AutenticarUsuarioAsync(string usuario, string password)
        {
            // AQUÍ LA LÓGICA DE AUTENTICACIÓN //

            var search = await _context.Users.FirstOrDefaultAsync(u => u.Username == usuario && u.Password == password);
            Console.WriteLine("Search=> " + search);
            if (search!=null)
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

        // GENERAMOS EL TOKEN CON LA INFORMACIÓN DEL USUARIO
        private string GenerarTokenJWT(Users usuarioInfo)
        {
            // CREAMOS EL HEADER //
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:ClaveSecreta"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            // CREAMOS LOS CLAIMS //
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, usuarioInfo.Id.ToString()),
                new Claim("nombre", usuarioInfo.FirstName),
                new Claim("apellidos", usuarioInfo.LastName),
                new Claim("username", usuarioInfo.Username),
                new Claim(ClaimTypes.Role, usuarioInfo.Rol)
            };

            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 24 horas.
                    expires: DateTime.UtcNow.AddHours(24)
                );

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }

    }
}