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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var granos = new List<Grano>()
            {
                new Grano() {Id=1,Descripcion="Soja"},
                new Grano() {Id=2,Descripcion="Trigo"},
                new Grano() {Id=3,Descripcion="Maiz"}

            };
            builder.Entity<Grano>().HasData(granos);
            var parametros = new List<Parametro>()
            {

                new Parametro(){ Id = 1, Riesgo = "Alto", CO2Value = 10, HumedadValue = 16, TemperaturaValue = 26 ,GranoID=1},
                new Parametro(){Id=2,Riesgo="Medio",CO2Value=10,HumedadValue=14,TemperaturaValue=24,GranoID=1},
                new Parametro(){Id=3,Riesgo="Bajo",CO2Value=10,HumedadValue=12,TemperaturaValue=22, GranoID = 1}
            };
            builder.Entity<Parametro>().HasData(parametros);

            var estados = new List<Estado>()
            {
                new Estado(){Id=1,Descripcion="Alerta"},
                new Estado(){Id=2,Descripcion="SinDatos"},
                new Estado(){Id=3,Descripcion="Ok"},
                new Estado(){Id=4,Descripcion="Advertencia"}
            };

            builder.Entity<Estado>().HasData(estados);

            base.OnModelCreating(builder);
        }

        //public override int SaveChanges()
        //{
        //    var entidades = ChangeTracker.Entries();
        //    if (entidades != null)
        //    {
        //        foreach (var entidad in entidades.Where(c => c.State != EntityState.Unchanged))
        //        {
        //            //TODO: auditar(entidad)
        //        }
        //    }
        //    return base.SaveChanges(); 
        //}

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
