
using System.Collections.Generic;

namespace SecureSilo.Shared
{
    public class ResponseSuscripcion
    {
        public string User { get; set; }
        public List<Suscripcion> Suscripciones { get; set; }
    }
}
