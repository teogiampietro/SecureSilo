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
        private Silo silo = new Silo();
        private List<Estado> estados = new List<Estado>();
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
            string[] updateList = jsonUpdateList.Split(";");
            //va a la controladora de dispositivos y inicializa el contexto
            DispositivosController cDispositivos = new DispositivosController(context);
            //en un objeto silo, graba la mac para luego buscar por la misma
            Silo _silo = JsonSerializer.Deserialize<Silo>(updateList[0]);      
           
            try
            {
                if (!SiloExist(_silo))
                {
                    //si no encuentro silo creo uno
                    Silo newSilo = new Silo();
                    newSilo.Descripcion = "SIN_ASIGNAR";
                    newSilo.MAC = _silo.MAC;
                    newSilo.Estado = estados[0];
                    SilosController cSilo = new SilosController(context);
                    //aca le pega a la base con el silo nuevo creado
                    await cSilo.Post(newSilo);
                    //se trae el silo nuevo creado y este es el silo con el que vamos a trabajar
                    silo = context.Silos.Where(a => a.MAC == newSilo.MAC).FirstOrDefault();
                }
                //traigo los estados de la base
                estados = context.Estados.ToList();

                //remuevo el primer elemento, porque es un elemento de tipo silo
                updateList = updateList.Where((source, index) => index != 0).ToArray();

                foreach (var item in updateList)
                {
                    Update update = JsonSerializer.Deserialize<Update>(item.ToString());
                    Dispositivo dsp = FindDispositivo(update.M);
                    if (dsp == null)
                    {
                        dsp = new Dispositivo
                        {
                            MAC = update.M,
                            Descripcion = string.Empty,
                            Silo = silo,
                            Estado = estados[0]
                        };
                        await cDispositivos.Post(dsp);
                    }
                    update.Dispositivo = dsp;
                    update.Dispositivo.Descripcion = ("dsp" + dsp.Id);
                    update.Dispositivo.Estado = CalcularEstadoUpdate(update, silo.Grano);
                    update.DispositivoID = dsp.Id;
                    update.F = DateTime.Now.ToString();
                    this.context.Add(update);
                }
                _silo.Estado = CalcularEstadoSilo(silo);
                context.Entry(_silo).State = EntityState.Modified;
            }
            catch (Exception e)
            { 
                throw new InvalidOperationException(e.Message);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }
        #region Private Methods
        private Dispositivo FindDispositivo(string MAC)
        {
            Dispositivo newDispositivo = new Dispositivo();
            newDispositivo = context.Dispositivos.Where(x => x.MAC == MAC).FirstOrDefault();
            return newDispositivo;
        }
        private Estado CalcularEstadoUpdate(Update upd, Grano grano)
        {
            if (grano.Parametros == null)
            {
                grano.Parametros = context.Parametros.Where(a => a.GranoID == grano.Id).ToList();
            }
            if (String.IsNullOrEmpty(upd.A) || upd.T != double.MinValue || upd.H != double.MinValue)
            {
                if (upd.A == Constants.Ok)
                {
                    foreach (var item in grano.Parametros)
                    {
                        if (upd.H >= item.HumedadMinValue && 
                            upd.H < item.HumedadMaxValue)
                        {
                            switch (item.Riesgo)
                            {
                                case Constants.Alto:
                                    return estados[3]; //alerta
                                case Constants.Medio:
                                    return estados[2]; //advertencia
                                case Constants.Bajo:
                                    return estados[1]; //ok
                                default:
                                    return estados[4];
                            }
                        }
                    }
                }
                else
                {
                    return estados[4]; //Alerta
                }

                return estados[4]; //Alerta
            }

            return estados[1];
        }
        private Estado CalcularEstadoSilo(Silo silo)
        {
            List<Dispositivo> dispositivosRevisar = new List<Dispositivo>();
            dispositivosRevisar = silo.Dispositivos.Where(a => a.Estado == estados[3]).ToList();
            dispositivosRevisar = silo.Dispositivos.Where(b => b.Estado == estados[2]).ToList();

            if (dispositivosRevisar != null && dispositivosRevisar.Count > 0)
            {
                if (dispositivosRevisar.Where(x => x.Estado == estados[3]) != null)
                {
                    return estados[3];
                }
                else if (dispositivosRevisar.Where(x => x.Estado == estados[2]) != null)
                {
                    return estados[2];
                }
                else
                {
                    return estados[1];
                }
            }
            return estados[1];
        }

        private bool SiloExist(Silo _silo)
        {
            silo = context.Silos
                .Include(a => a.Campo)
                .Include(b => b.Grano.Parametros)
                .Include(c => c.Estado)
                .Where(x => x.MAC == _silo.MAC).FirstOrDefault();
            if (silo == null)
                return false;
            else
                return true;
        }
        #endregion

    }
}
