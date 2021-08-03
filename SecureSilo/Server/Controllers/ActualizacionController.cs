using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SecureSilo.Shared;
using SecureSilo.Server.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using SecureSilo.Shared.Identity;

namespace SecureSilo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ActualizacionController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Silo silo = new Silo();
        private List<Estado> estados = new List<Estado>();
        private bool flag;
        public ActualizacionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
            flag = false;
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
        public async Task<ActionResult<Actualizacion>> Get(int id)
        {
            return await context.Actualizaciones.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet("getFiltered/{nombreDispositivoFiltro}")]
        public async Task<ActionResult<List<Dispositivo>>> Get(string nombreDispositivoFiltro)
        {
            return await context.Dispositivos.Include(x => x.Updates).Where(y => y.Descripcion == nombreDispositivoFiltro).ToListAsync();
        }
        //Recibe un string de updates para un determiado silo
        [HttpPost]
        public async Task<ActionResult> Post([FromQuery] string jsonUpdateList)
        {
            if (string.IsNullOrEmpty(jsonUpdateList))
            {
                return new BadRequestResult();
            }
            string[] updateList = jsonUpdateList.Split(";");
            //va a la controladora de dispositivos y inicializa el contexto
            DispositivosController cDispositivos = new DispositivosController(context, _userManager);
            //en un objeto silo, graba la mac para luego buscar por la misma
            Silo _silo = JsonSerializer.Deserialize<Silo>(updateList[0]);
            estados = context.Estados.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                if (!SiloExist(_silo))
                {
                    //si no encuentro silo creo uno
                    Silo newSilo = new Silo
                    {
                        Descripcion = "SIN_ASIGNAR",
                        MAC = _silo.MAC,
                        Estado = estados[0],
                        UserId = userId
                    };
                    SilosController cSilo = new SilosController(context, _userManager);
                    //aca le pega a la base con el silo nuevo creado
                    await cSilo.Post(newSilo);
                    //se trae el silo nuevo creado y este es el silo con el que vamos a trabajar
                    silo = context.Silos.Where(a => a.MAC == newSilo.MAC)
                                        .FirstOrDefault();
                }
                //remuevo el primer elemento, porque es un elemento de tipo silo
                updateList = updateList.Where((source, index) => index != 0)
                                       .ToArray();

                foreach (var item in updateList)
                {
                    Actualizacion update = JsonSerializer.Deserialize<Actualizacion>(item.ToString());
                    Dispositivo dsp = FindDispositivo(update.M);
                    if (dsp == null)
                    {
                        
                        dsp = new Dispositivo
                        {
                            MAC = update.M,
                            Descripcion = string.Empty,
                            Silo = silo,
                            SiloId = silo.Id,
                            Estado = estados[0],
                            UserId = userId
                        };
                        await cDispositivos.Post(dsp);
                    }
                    update.Dispositivo = dsp;
                    update.Dispositivo.Descripcion = ("dsp" + dsp.Id);
                    update.Dispositivo.Estado = CalcularEstadoUpdate(update, silo.Grano);
                    update.DispositivoID = dsp.Id;
                    update.F = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                    this.context.Add(update);
                }
                await context.SaveChangesAsync();
                silo.Estado = CalcularEstadoSilo(silo);
                context.Entry(silo).State = EntityState.Modified;
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
            newDispositivo = context.Dispositivos.Where(x => x.MAC == MAC && x.SiloId == silo.Id).FirstOrDefault();
            return newDispositivo;
        }
        private Estado CalcularEstadoUpdate(Actualizacion upd, Grano grano)
        {
            if (grano.Parametros == null)
            {
                grano.Parametros = context.Parametros.Where(a => a.GranoID == grano.Id).ToList();
            }
            if (String.IsNullOrEmpty(upd.A) || upd.T != double.MinValue || upd.H != double.MinValue)
            {
                if (upd.A != Constants.Ok)
                {
                    return estados[3]; //Alerta
                }
                else
                {
                    foreach (var item in grano.Parametros)
                    {
                        if (upd.H > item.HumedadMinValue &&
                            upd.H <= item.HumedadMaxValue)
                        {
                            switch (item.Riesgo)
                            {
                                case Constants.Alto:
                                    return estados[2]; //advertencia
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
                return estados[3]; //Alerta
            }

            return estados[1];
        }
        private Estado CalcularEstadoSilo(Silo silo)
        {
            List<Dispositivo> dispositivosRevisar = new List<Dispositivo>();
            dispositivosRevisar.AddRange(silo.Dispositivos.Where(a => a.Estado == estados[3]).ToList());
            dispositivosRevisar.AddRange(silo.Dispositivos.Where(b => b.Estado == estados[2]).ToList());

            if (dispositivosRevisar != null && dispositivosRevisar.Count > 0)
            {
                if (dispositivosRevisar.Any(x => x.Estado == estados[3]))
                {
                    if (flag == false)
                        EnviarMailAsync();

                    return estados[3];
                }
                else if (dispositivosRevisar.Any(x => x.Estado == estados[2] ))
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

        private string ArmarBodyEmail()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Sistema " + Constants.nombreEmpresa);
            sb.AppendLine("Informe de alerta:");
            sb.Append("Le informamos que en el campo " + silo.Campo.Descripcion);
            sb.Append(", el silo " + silo.Descripcion);
            sb.AppendLine(" en el ultimo recibo de actualizacion recibio una falla.");
            sb.AppendLine("Recomendamos tomar acciones.");
            sb.AppendLine("Localidad:" + silo.Campo.Localidad);
            sb.AppendLine("Ubicacion:" + silo.Campo.Ubicacion);
            sb.AppendLine("");
            sb.AppendLine("Servicio de Soporte " + Constants.nombreEmpresa);
            return sb.ToString();
        }

        private void EnviarMailAsync()
        {
            MailServiceController mail = new MailServiceController(context, _userManager);
            //capturar mail según dueño del campo
            var mailCampo = context.Users.Where(x => x.Id == silo.UserId).FirstOrDefault();

            var resultado = mail.SendMessage(mailCampo.Email, "SISTEMA " + Constants.nombreEmpresa.ToUpper(), ArmarBodyEmail());
            if (resultado == true)
                flag = true;
        }
        #endregion

    }
}
