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

        // TRAEMOS EL OBJETO DE CONFIGURACIÓN (appsettings.json)
        // MEDIANTE INYECCIÓN DE DEPENDENCIAS.
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
            var _userInfo = await AutenticarUsuarioAsync(usuarioLogin.Usuario, usuarioLogin.Password);
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


            // Supondremos que el Usuario existe en la Base de Datos.
            // Retornamos un objeto del tipo UsuarioInfo, con toda
            // la información del usuario necesaria para el Token.
            var search = await _context.Users.FirstOrDefaultAsync(u => u.Username == usuario && u.Password == password);
            Console.WriteLine(search);
            Console.WriteLine(usuario);
            Console.WriteLine(password);
            return search;
            
            /*return new Users()
            {
                // Id del Usuario en el Sistema de Información (BD)
                Id = 22,
                FirstName = "Nombre Usuario",
                LastName = "Apellidos Usuario",
                Username = "email.usuario@dominio.com",
                Rol = "Administrador"
            };*/

            // Supondremos que el Usuario NO existe en la Base de Datos.
            // Retornamos NULL.
            //return null;
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