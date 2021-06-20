using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureSilo.Shared
{
   public class Update
    {
        [Key]
        public int Id { get; set; }
        public string M { get; set; }
        public string F { get; set; }
        public string A { get; set; }
        public double T { get; set; }
        public double H { get; set; }
        public double C { get; set; }
        public int DispositivoID { get; set; }
        public Dispositivo Dispositivo { get; set; }

    }
}
