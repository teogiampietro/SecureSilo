using System;
using System.Collections.Generic;
using System.Text;

namespace SecureSilo.Shared
{
    public class RequestEstadisticas
    {
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int?  SiloId { get; set; }
        public string Movimiento { get; set; }
    }
}
