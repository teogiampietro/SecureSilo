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
    public class DispositivosController : ControllerBase
    {

        public readonly ApplicationDbContext context;
        public DispositivosController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public async Task<ActionResult<List<Dispositivo>>> Post(Dispositivo dispositivo)
        {
            //TODO: agregar validacion que el silo no puede tener más de 10 dispositivos
            context.Add(dispositivo);
            await context.SaveChangesAsync();
            if (String.IsNullOrEmpty(dispositivo.Descripcion))
            {
                return new CreatedAtRouteResult("obtenerDispositivo", new { id = dispositivo.Id, descripcion = ("dsp" + dispositivo.Id) }, dispositivo);
            }
            else
            {
                return new CreatedAtRouteResult("obtenerDispositivo", new { id = dispositivo.Id }, dispositivo);
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dispositivo = new Dispositivo { Id = id };
            context.Remove(dispositivo);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult> Put(Dispositivo dispositivo)
        {
            context.Entry(dispositivo).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        #region GET
        [HttpGet]
        public async Task<ActionResult<List<Dispositivo>>> Get()
        {
            return await context.Dispositivos.Include(x => x.Silo).Include(y => y.Silo.Campo).ToListAsync();
        }
        [HttpGet("{id}", Name = "obtenerDispositivo")]
        public async Task<ActionResult<Dispositivo>> Get(int id)
        {
            return await context.Dispositivos.Include(x => x.Silo).FirstOrDefaultAsync(x => x.Id == id);
        }
        [HttpGet("GetDispositivoPorSilo/{idSilo}", Name = "obtenerDispositivoxSilo")]
        public async Task<ActionResult<List<Dispositivo>>> GetDispositivosPorSilo(int idSilo)
        {
            return await context.Dispositivos   
                .Include(x => x.Updates)
                .Where(x => x.SiloId == idSilo)
                .ToListAsync();
        }
        #endregion


    }
}
