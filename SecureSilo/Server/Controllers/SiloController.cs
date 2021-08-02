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
using SecureSilo.Shared.Identity;

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
        #region CUD
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
                if (string.IsNullOrEmpty(silo.UserId))
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
        #endregion
        #region GETTERS
        //Endpoint para obtener todos los silos
        [HttpGet]
        public async Task<ActionResult<List<Silo>>> Get()
        {
            var asd = await context.Silos
                .Include(a => a.Dispositivos).ThenInclude(x => x.Estado)
                .Include(b => b.Campo)
                .Include(c => c.Grano)
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToListAsync();
            return asd;
        }
        //Endpoint para obtener un silo por id
        [HttpGet("{id}", Name = "obtenerSilo")]
        public async Task<ActionResult<Silo>> Get(int id)
        {
            return await context.Silos
                .Include(a => a.Grano)
                .Include(b => b.Campo)
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        //Endpoint para traer una lista de silos que pertenecen a un campo
        [HttpGet("GetSiloxCampo/{campoId}", Name = "GetSiloxCampo")]
        public async Task<ActionResult<List<Silo>>> GetSiloxCampo(int campoId)
        {
            var response = await context.Silos
                .Include(a => a.Grano.Parametros)
                .Include(b => b.Campo)
                .Include(c => c.Dispositivos).ThenInclude(c => c.Estado)
                .Include(d => d.Estado)
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Where(x => x.CampoID == campoId)
                .ToListAsync();

            return response;
        }

        //Endpoint para las gráficas de la ventana estadísticas
        [HttpGet("GetSiloChart/{idSilo}&{dias}")]
        public async Task<ActionResult<ResponseChart>> GetSiloChart(int idSilo, int dias = 30)
        {           
            ResponseChart response = new ResponseChart();
            var updatesFiltrados = (from updates in this.context.Actualizaciones
                                    join dispositivos in context.Dispositivos on updates.DispositivoID equals dispositivos.Id
                                    where dispositivos.SiloId == idSilo
                                    && dispositivos.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)
                                    group updates by updates.F into g
                                    orderby g.Key
                                    select new
                                    {
                                        g.Key,
                                        temperatura = g.Average(x => x.T),
                                        co2 = g.Average(x => x.C),
                                        humedad = g.Average(x => x.H)
                                    }
                               ).Take(dias);

            response.humedad = await updatesFiltrados.Select(x => x.humedad).ToArrayAsync();
            response.co2 = await updatesFiltrados.Select(x => x.co2).ToArrayAsync();
            response.temperatura = await updatesFiltrados.Select(x => x.temperatura).ToArrayAsync();
            return response;
        }
        #endregion
    }
}
