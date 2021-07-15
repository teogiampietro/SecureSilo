using Microsoft.AspNetCore.Identity;
using SecureSilo.Shared;
using System.Collections.Generic;
namespace SecureSilo.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Campo> Campos { get; set; }
        public List<Suscripcion> Suscripciones { get; set; }
    }
}
