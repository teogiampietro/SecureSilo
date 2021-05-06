using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SecureSilo.Shared
{
    public class Panel
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        //Un panel tiene una lista de silos
        public List<Silo> ListaSilos { get; set; }
        //Un panel tiene una lista de usuarios
       // public List<> ListaUsuarios { get; set; }
    }
}
