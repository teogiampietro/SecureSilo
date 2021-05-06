using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebApp.Shared
{
    public class Silo
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        //Un silo tiene una lista de dispositivos
        public List<Dispositivo> ListaDispositivos { get; set; }

        //Un silo se encuentra en un panel
        [ForeignKey("Panel")]
        public int PanelID { get; set; }
        public Panel Panel { get; set; }
    }
}
