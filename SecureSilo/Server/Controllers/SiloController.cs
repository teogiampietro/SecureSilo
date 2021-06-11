using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SecureSilo.Shared;
using SecureSilo.Server.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace SecureSilo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SilosController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        public SilosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Silo>>> Get()
        {
            return await context.Silos
                .Include("Dispositivos.Estado")
                .Include(y => y.Campo)
                .Include(z => z.Grano.Parametros)
                .Include(a => a.Estado)
                .ToListAsync();
        }
        [HttpGet("{id}", Name = "obtenerSilo")]
        public async Task<ActionResult<Silo>> Get(int id)
        {
            return await context.Silos.FirstOrDefaultAsync(x => x.Id == id);
        }
        [HttpPost]
        public async Task<ActionResult> Post(Silo silo)
        {
            context.Add(silo);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("obtenerSilos", new { id = silo.Id }, silo);
        }
        [HttpPut]
        public async Task<ActionResult> Put(Silo silo)
        {
            context.Entry(silo).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var silo = new Silo { Id = id };
            context.Remove(silo);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
