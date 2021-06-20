using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureSilo.Shared
{
    public class Silo
    {
        [Key]
        public int Id { get; set; }
        public string MAC { get; set; }
        public string Descripcion { get; set; }

        //un silo tiene un estado actual
        public int? EstadoId { get; set; }
        public Estado Estado { get; set; }

        //Un silo se encuentra en un campo
        public int CampoID { get; set; }
        public Campo Campo { get; set; }

        //Un silo posee un tipo de grano
        public int GranoID { get; set; }
        public Grano Grano { get; set; }

        //Un silo tiene una lista de dispositivos
        public List<Dispositivo> Dispositivos { get; set; }




    }
}
