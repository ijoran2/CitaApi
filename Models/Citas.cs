using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaAPI.Models
{
    public class Citas
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
    }
}