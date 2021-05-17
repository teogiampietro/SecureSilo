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
    public class PanelesController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        public PanelesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Panel>>> Get()
        {
            return await context.Paneles.ToListAsync();
        }
        [HttpGet("{id}", Name = "obtenerPanel")]
        public async Task<ActionResult<Panel>> Get(int id)
        {
            return await context.Paneles.FirstOrDefaultAsync(x => x.Id == id);
        }
        [HttpGet("{panelId}/silos")]
        public async Task<List<Silo>> GetSilos(int panelId)
        {
            return await context.Silos.Where(x => x.PanelID == panelId)
                .OrderBy(x => x.Descripcion).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Post(Panel panel)
        {
            context.Add(panel);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("obtenerPaneles", new { id = panel.Id }, panel);
        }

        public async Task<ActionResult> Put(Panel panel)
        {
            context.Entry(panel).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
