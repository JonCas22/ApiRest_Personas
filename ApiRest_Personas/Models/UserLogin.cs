using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest_Personas.Models
{
    public class UserLogin
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
