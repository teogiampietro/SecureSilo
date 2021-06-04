using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureSilo.Shared
{
    public class Dispositivo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string NumeroSerie { get; set; }
        public string Descripcion { get; set; }          
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un silo")]
        public int SiloID {get; set; }
        public Silo Silo { get; set; }
        public List<Update> ListaUpdates { get; set; }
        public string Estado { get; set; }

    }
}
