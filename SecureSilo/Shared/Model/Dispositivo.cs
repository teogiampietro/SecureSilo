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
        public string MAC { get; set; }
        public string Descripcion { get; set; }

        public int? EstadoId { get; set; }
        public Estado Estado { get; set; }

        public int SiloId {get; set; }
        public Silo Silo { get; set; }
        
        //dispositivo ... updates
        public List<Actualizacion> Updates { get; set; }
        public string UserId { get; set; }

    }
}
