using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SecureSilo.Shared.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public List<Campo> Campos { get; set; }
        public List<Suscripcion> Suscripciones { get; set; }
    }
}
