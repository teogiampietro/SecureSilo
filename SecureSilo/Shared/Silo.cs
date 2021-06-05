﻿using System;
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
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        //Un silo tiene una lista de dispositivos
        public List<Dispositivo> Dispositivos { get; set; }

        //Un silo se encuentra en un campo
        public int CampoID { get; set; }
        public Campo Campo { get; set; }
    }
}
