using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SecureSilo.Shared;
using SecureSilo.Server.Data;

namespace SecureSilo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CamposController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        public CamposController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Campo>>> Get()
        {
            return await context.Campos.ToListAsync();
        }
        [HttpGet("{id}", Name = "obtenerCampo")]
        public async Task<ActionResult<Campo>> Get(int id)
        {
            return await context.Campos.FirstOrDefaultAsync(x => x.Id == id);
        }
        [HttpGet("{campoId}/silos")]
        public async Task<List<Silo>> GetSilos(int campoId)
        {
            return await context.Silos.Where(x => x.CampoID == campoId)
                .OrderBy(x => x.Descripcion).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Post(Campo campo)
        {
            context.Add(campo);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("obtenerCampo", new { id = campo.Id }, campo);
        }
        [HttpPut]
        public async Task<ActionResult> Put(Campo campo)
        {
            context.Entry(campo).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
       [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var campo = new Campo { Id = id };
            context.Remove(campo);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
