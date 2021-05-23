using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SecureSilo.Shared;
using SecureSilo.Server.Data;
using System.Text.Json;

namespace SecureSilo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        public UpdateController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Update>>> Get()
        {
            return await context.Updates.Include(x => x.Dispositivo.Silo.Panel).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Post(string jsonUpdateList)
        {
            try
            { 
                string[] updateList = jsonUpdateList.Split(";");
                foreach (var item in updateList)
                {
                    Update update = JsonSerializer.Deserialize<Update>(item);
                    Dispositivo dsp = FindDispositivo(update.NumeroSerie);
                    update.Dispositivo = dsp;
                    update.DispositivoID = dsp.Id;
                    this.context.Add(update);
                }
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }   
        }
        [HttpGet("{id}", Name = "obtenerUpdate")]
        public async Task<ActionResult<Update>> Get(int id)
        {
            return await context.Updates.FirstOrDefaultAsync(x => x.Id == id);
        }
        private Dispositivo FindDispositivo(string numeroSerie)
        {
            Dispositivo newDispositivo = new Dispositivo();
            newDispositivo =  context.Dispositivos.Where(x => x.NumeroSerie == numeroSerie).FirstOrDefault();
            return newDispositivo;
        }
    }
}
