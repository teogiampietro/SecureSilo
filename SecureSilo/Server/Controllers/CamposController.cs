using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureSilo.Server.Data;
using SecureSilo.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SecureSilo.Server.Models;

namespace SecureSilo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CamposController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        public CamposController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<List<Campo>>> Get()
        {
            return await context.Campos.Include(a=>a.Localidad.Provincia)
                 .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                 .ToListAsync();
        }
        [HttpGet("{id}", Name = "obtenerCampo")]
        public async Task<ActionResult<Campo>> Get(int id)
        {
            return await context.Campos
                .Include(a => a.Localidad.Provincia)
                .Where(x=>x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync(x => x.Id == id);
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            campo.UserId = userId;
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
