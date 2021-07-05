using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SecureSilo.Shared;
using SecureSilo.Server.Data;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SecureSilo.Server.Models;

namespace SecureSilo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SilosController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        public SilosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult> Post(Silo silo)
        {
            try
            {
                if (silo.Campo == null && silo.Grano == null)
                {
                    silo.Grano = context.Granos.FirstOrDefault();
                    silo.Campo = context.Campos.FirstOrDefault();
                    if (silo.Estado == null)
                        silo.Estado = context.Estados.FirstOrDefault();
                }
                silo.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                context.Add(silo);
                await context.SaveChangesAsync();
                return new CreatedAtRouteResult("obtenerSilos", new { id = silo.Id }, silo);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

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

        [HttpGet]
        public async Task<ActionResult<List<Silo>>> Get()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var asd = await context.Silos
                .Include("Dispositivos.Estado")
                .Include(y => y.Campo)
                .Include(z => z.Grano.Parametros)
                .Include(a => a.Estado)
                .Where(x => x.UserId== User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToListAsync();
            return asd;
        }
        [HttpGet("{id}", Name = "obtenerSilo")]
        public async Task<ActionResult<Silo>> Get(int id)
        {
            return await context.Silos
                .Include(a => a.Grano)
                .Include(b => b.Campo)
                .Where(x => x.UserId== User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        [HttpGet("GetSiloxCampo/{campoId}", Name = "GetSiloxCampo")]
        public async Task<ActionResult<List<Silo>>> GetSiloxCampo(int campoId)
        {
            return await context.Silos
                .Include(a => a.Grano)
                .Include(b => b.Campo)
                .Include(c => c.Dispositivos).ThenInclude(y => y.Estado)
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Where(x => x.CampoID == campoId)
                .ToListAsync();
        }
        [HttpGet("GetSiloChart/{idSilo}")]
        public async Task<ActionResult<ResponseChart>> GetSiloChart(int idSilo)
        {
            ResponseChart response = new ResponseChart();
            var updatesFiltrados = (from updates in this.context.Actualizaciones
                                    join dispositivos in context.Dispositivos on updates.DispositivoID equals dispositivos.Id
                                    where dispositivos.SiloId == idSilo 
                                    && dispositivos.UserId ==  User.FindFirstValue(ClaimTypes.NameIdentifier)
                                    orderby updates.F descending
                                    select updates
                               ).Take(30);

            response.humedad = updatesFiltrados.Select(x => x.H).ToArray();
            response.co2 = updatesFiltrados.Select(x => x.C).ToArray();
            response.temperatura = updatesFiltrados.Select(x => x.T).ToArray();
            return response;
        }

    }
}
