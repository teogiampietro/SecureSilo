using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebApp.Shared
{
    public class Dispositivo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string NumeroSerie { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Descripcion { get; set; }
        public string Movimiento { get; set; }
        public float Temperatura { get; set; }
        public float Humedad { get; set; }
        public DateTime FechaHora { get; set; }
       
        //Un dispositivo se encuentra en un silo.
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un silo")]
        public int SiloID {get; set; }
        public Silo Silo { get; set; }
    }
}
