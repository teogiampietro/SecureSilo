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
            string[] updateList = jsonUpdateList.Split(";");
            DispositivosController cDispositivos = new DispositivosController(context);
            Silo _silo = JsonSerializer.Deserialize<Silo>(updateList[0]);
            _silo = SiloExist(_silo);
            try
            {
                if (_silo == null)
                {
                    //si no encuentro silo tengo que crear uno
                    _silo = new Silo
                    {

                    };
                }

                //si encontró silo, ahora tengo que actualizar los dispositivos.
                updateList = updateList.Where((source, index) => index != 0).ToArray(); //remuevo el primer elemento, porque es un elemento de tipo silo
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
                            Silo = _silo,
                            Estado = CalcularEstadoUpdate(update, _silo.Grano)
                        };
                        await cDispositivos.Post(dsp);
                    }
                    update.Dispositivo = dsp;
                    update.Dispositivo.Descripcion = ("dsp" + dsp.Id);
                    update.Dispositivo.Estado = CalcularEstadoUpdate(update, _silo.Grano);
                    update.DispositivoID = dsp.Id;
                    update.F = DateTime.Now.ToString();
                    this.context.Add(update);
                }

            }
            catch (Exception)
            {

                throw;
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
        private Estado CalcularEstadoUpdate(Update upd, Grano grano)
        {

            List<Estado> _estados = context.Estados.ToList();

            if (String.IsNullOrEmpty(upd.A) || upd.T != double.MinValue || upd.H != double.MinValue)
            {
                if (upd.A == "ok")
                {
                    foreach (var item in grano.Parametros)
                    {
                        if (upd.H >= item.HumedadValue)
                        {
                            switch (item.Riesgo)
                            {
                                case "Alto":
                                    return _estados[0];
                                case "Medio":
                                    return _estados[3];
                                case "Bajo":
                                    return _estados[2];
                                default:
                                    return _estados[1];
                            }
                        }
                    }
                }
                else
                {
                    return _estados[0]; //Alerta
                }
                return _estados[0]; //Alerta

                //grano.Parametros.Where(a => a.Riesgo == "Alto").Select(b => b.HumedadValue);
            }
            return _estados[1];
        }


        private Silo SiloExist(Silo _silo)
        {
            _silo = context.Silos.Include(a => a.Campo)
                .Include(b => b.Grano.Parametros)
                .Include(c => c.Estado)
                .Where(x => x.MAC == _silo.MAC).FirstOrDefault();
            return _silo;
        }
        #endregion

    }
}
