using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecureSilo.Shared
{
    public class AuditClass
    {
        public int Id { get; set; }
        public string Accion { get; set; }
        public DateTime Fecha { get; set; }
        public string User { get; set; }
        public string Clase { get; set; }
        public string Data { get; set; }
    }
}
