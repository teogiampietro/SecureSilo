using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SecureSilo.Shared
{
    public class Campo
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        //tiene una lista de silos
        public List<Silo> ListaSilos { get; set; }
        //tiene un usuario
        public int UserId { get; set; }
        
    }
}
