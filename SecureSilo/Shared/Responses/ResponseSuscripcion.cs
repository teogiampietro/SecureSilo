
using SecureSilo.Shared.Identity;
using System.Collections.Generic;

namespace SecureSilo.Shared
{
    public class ResponseSuscripcion
    {
        public ApplicationUser User { get; set; }
        public List<Suscripcion> Suscripciones { get; set; }
    }
}
