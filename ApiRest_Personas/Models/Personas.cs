using System;
using System.Collections.Generic;

namespace ApiRest_Personas.Models
{
    public partial class Personas
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public int Phone { get; set; }
        public string Province { get; set; }
    }
}
