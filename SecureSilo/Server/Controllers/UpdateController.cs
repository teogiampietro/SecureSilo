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
        [HttpGet(Name = "listaUpdates")]
        public async Task<ActionResult<List<Dispositivo>>> Get()
        {
            return await context.Dispositivos
                .Include(x => x.ListaUpdates)
                .Include(y => y.Silo.Panel)
                .Where(z => z.ListaUpdates.Count > 0)
                .ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Post(string jsonUpdateList)
        {
            DispositivosController cDispositivos = new DispositivosController(context);
            string[] updateList = jsonUpdateList.Split(";");
            foreach (var item in updateList)
            {
                try
                {
                    Update update = JsonSerializer.Deserialize<Update>(item);
                    Dispositivo dsp = FindDispositivo(update.NumeroSerie);
                    if (dsp == null)
                    {
                        //Si no encuentro el dispositivo por numero de serie, quiere decir que no está cargado.
                        //Así que creo un nuevo dispositivo con cualquier silo asignado. Deberá configurar el usuario.
                        dsp = new Dispositivo();
                        dsp.NumeroSerie = update.NumeroSerie;
                        dsp.Descripcion = string.Empty;
                        dsp.Silo = FindFirstSilo();
                        await cDispositivos.Post(dsp);
                    }
                    update.Dispositivo = dsp;
                    update.Dispositivo.Descripcion = ("dsp" + dsp.Id);
                    update.DispositivoID = dsp.Id;
                    update.FechaHora = DateTime.Now.ToString();
                    this.context.Add(update);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("{id}", Name = "obtenerUpdate")]
        public async Task<ActionResult<Update>> Get(int id)
        {
            return await context.Updates.FirstOrDefaultAsync(x => x.Id == id);
        }
        private Dispositivo FindDispositivo(string numeroSerie)
        {
            Dispositivo newDispositivo = new Dispositivo();
            newDispositivo = context.Dispositivos.Where(x => x.NumeroSerie == numeroSerie).FirstOrDefault();
            return newDispositivo;
        }
        private Silo FindFirstSilo()
        {
            Silo newSilo = new Silo();
            newSilo = context.Silos.FirstOrDefault();
            return newSilo;
        }
    }
}
