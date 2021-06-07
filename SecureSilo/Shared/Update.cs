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
        public string NumeroSerie { get; set; }
        public string FechaHora { get; set; }
        public string Movimiento { get; set; }
        public float Temperatura { get; set; }
        public float Humedad { get; set; }
        public int DispositivoID { get; set; }
        public Dispositivo Dispositivo { get; set; }

    }
}
