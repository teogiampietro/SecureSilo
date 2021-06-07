using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureSilo.Shared;

namespace SecureSilo.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Campo> Campos { get; set; }
    }
}
