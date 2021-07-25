using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecureSilo.Shared;
using SecureSilo.Shared.Identity;
using System.Collections.Generic;

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
                new Parametro(){Id=1,Riesgo= Constants.Alto,TemperaturaValue=35,HumedadMinValue=14,HumedadMaxValue=99, GranoID=1},
                new Parametro(){Id=2,Riesgo= Constants.Medio,TemperaturaValue=35,HumedadMinValue=10,HumedadMaxValue=14,GranoID=1},
                new Parametro(){Id=3,Riesgo= Constants.Bajo,TemperaturaValue=35,HumedadMinValue=0,HumedadMaxValue=10, GranoID = 1},
                new Parametro(){Id=4,Riesgo= Constants.Alto,TemperaturaValue=35,HumedadMinValue=14,HumedadMaxValue=99, GranoID = 2},
                new Parametro(){Id=5,Riesgo= Constants.Medio,TemperaturaValue=35,HumedadMinValue=10,HumedadMaxValue=14, GranoID = 2},
                new Parametro(){Id=6,Riesgo= Constants.Bajo,TemperaturaValue=35,HumedadMinValue=0,HumedadMaxValue=10, GranoID = 2},
                new Parametro(){Id=7,Riesgo= Constants.Alto,TemperaturaValue=35,HumedadMinValue=14,HumedadMaxValue=99, GranoID = 3},
                new Parametro(){Id=8,Riesgo= Constants.Medio,TemperaturaValue=35,HumedadMinValue=10,HumedadMaxValue=14, GranoID = 3},
                new Parametro(){Id=9,Riesgo= Constants.Bajo,TemperaturaValue=35,HumedadMinValue=0,HumedadMaxValue=10, GranoID = 3},
            };
            builder.Entity<Parametro>().HasData(parametros);

            var estados = new List<Estado>()
            {
                new Estado(){Id = 1,Descripcion=Constants.Default, Riesgo=Constants.NoValue},
                new Estado(){Id=2,Descripcion=Constants.Ok, Riesgo=Constants.Bajo},
                new Estado(){Id=3,Descripcion=Constants.Advertencia, Riesgo=Constants.Medio},
                new Estado(){Id=4,Descripcion=Constants.Alerta, Riesgo = Constants.Alto},
                new Estado(){Id=5,Descripcion=Constants.SinEstado, Riesgo=Constants.NoValue }
            };
            builder.Entity<Estado>().HasData(estados);

            var categoria = new List<Categoria>()
            {
                new Categoria(){Id=1,Costo=2500,Descripcion="Base" },
                new Categoria(){Id=2,Costo=5000,Descripcion="Standar" },
                new Categoria(){Id=3,Costo=7000,Descripcion="Pro" },
                new Categoria(){Id=4,Costo=9000,Descripcion="Premium" }
            };
            builder.Entity<Categoria>().HasData(categoria);

            var formadepago = new List<FormaDePago>()
            {
                new FormaDePago(){Id =1, Descripcion="-- Seleccione una forma de pago --",CBU = "-",Alias = "-"},
                new FormaDePago(){Id =2, Descripcion="Efectivo",CBU = "-",Alias = "-"},
                new FormaDePago(){Id =3, Descripcion="Transferencia",CBU = "55948291235",Alias = "MACRO.SECURE.SILO"},
                new FormaDePago(){Id =4, Descripcion="Mercado Pago",CBU = "438850133263",Alias = "MP.SECURE.SILO" }
            };
            builder.Entity<FormaDePago>().HasData(formadepago);
            builder.Entity<Suscripcion>().HasKey(x => new { x.Id, x.Pagado });
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
        public DbSet<Actualizacion> Actualizaciones { get; set; }
        public DbSet<Grano> Granos { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Suscripcion> Suscripciones { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<FormaDePago> FormasDePagos { get; set; }
    }
}
