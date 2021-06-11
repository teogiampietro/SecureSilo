using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecureSilo.Server.Models;
using SecureSilo.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureSilo.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Silo> Silos { get; set; }
        public DbSet<Campo> Campos { get; set; }
        public DbSet<Update> Updates { get; set; } 
        public DbSet<Grano> Granos { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Estado> Estados { get; set; }

    }
}
