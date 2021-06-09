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
                .Include(x => x.Updates)
                .Include(y => y.Silo.Campo)
                .Where(z => z.Updates.Count > 0)
                .ToListAsync();
        }

        [HttpGet("{id}", Name = "obtenerUpdate")]
        public async Task<ActionResult<Update>> Get(int id)
        {
            return await context.Updates.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet("getFiltered/{nombreDispositivoFiltro}")]
        public async Task<ActionResult<List<Dispositivo>>> Get(string nombreDispositivoFiltro)
        {
            return await context.Dispositivos.Include(x => x.Updates).Where(y => y.Descripcion == nombreDispositivoFiltro).ToListAsync();
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
                        //Si no encuentro el dispositivo, quiere decir que no está cargado. Es un nuevo dispositivo en el sistema.
                        //Así que creo un nuevo dispositivo con un nuevo silo asignado. Deberá configurar el usuario.
                        dsp = new Dispositivo
                        {
                            MAC = update.NumeroSerie,
                            Descripcion = string.Empty,
                            Silo = FindFirstSilo()
                        };
                        await cDispositivos.Post(dsp);
                    }
                    //Si encuentro dispositivo es una actualización normal para un dispositivo ya cargado
                    update.Dispositivo = dsp;
                    update.Dispositivo.Descripcion = ("dsp" + dsp.Id);
                    update.Dispositivo.Estado = CalcularEstadoUpdate(update);
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
        #region Private Methods
        private Dispositivo FindDispositivo(string numeroSerie)
        {
            Dispositivo newDispositivo = new Dispositivo();
            newDispositivo = context.Dispositivos.Where(x => x.MAC == numeroSerie).FirstOrDefault();
            return newDispositivo;
        }
        private Silo FindFirstSilo()
        {
            Silo newSilo = new Silo();
            newSilo = context.Silos.FirstOrDefault();
            if (newSilo.Dispositivos.Count >= 10)
            {
                newSilo.Campo = context.Campos.FirstOrDefault();
                newSilo.Descripcion = "SinAsignar";
                newSilo.CampoID = newSilo.Campo.Id;
                context.Silos.Add(newSilo);
                context.SaveChanges();
                return newSilo;
            }
            return newSilo;
        }
        private string CalcularEstadoUpdate(Update upd)
        {
            if (upd.Movimiento != null || upd.Temperatura != double.MinValue || upd.Humedad != double.MinValue)
            {
                if (upd.Movimiento == "ok")
                {
                    if (upd.Temperatura <= Constants.temperaturaValue && upd.Humedad <= Constants.humedadValue)
                    {
                        return Constants.Ok;
                    }
                    else
                    {
                        if ((upd.Temperatura > Constants.temperaturaValue && upd.Temperatura < Constants.temperaturaMaxValue) ||
                             (upd.Humedad > Constants.humedadValue && upd.Temperatura < Constants.humedadMaxValue))
                        {
                            return Constants.Advertencia;
                        }
                        if (upd.Temperatura >= Constants.temperaturaMaxValue || upd.Humedad >= Constants.humedadMaxValue)
                        {
                            return Constants.Alerta;
                        }
                    }                  
                }
                else
                {
                    return Constants.Alerta;
                }
                return Constants.SinDatos;
            }
            return Constants.SinDatos;
        }
        #endregion

    }
}
