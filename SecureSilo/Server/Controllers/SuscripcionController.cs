using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureSilo.Server.Data;
using SecureSilo.Server.Helpers;
using SecureSilo.Shared;
using SecureSilo.Shared.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecureSilo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuscripcionController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Strategy strategy;
        public SuscripcionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this._userManager = userManager;
        }
        #region GETTERS
        [HttpGet]
        public async Task<ActionResult<List<Suscripcion>>> Get()
        {
            return await this.context.Suscripciones.Include(x => x.Categoria)
                .ToListAsync();
        }
        [HttpGet("{SubId}")]
        public async Task<ActionResult<Suscripcion>> Get(int SubId)
        {
            return await this.context.Suscripciones.Include(x => x.Categoria)
                .Where(x => x.Id == SubId)
                .FirstOrDefaultAsync();
        }
        [HttpGet("{UserId}")]
        public async Task<ActionResult<Suscripcion>> GetPorUserId(string UserId)
        {
            return await this.context.Suscripciones
                    .Include(x => x.Categoria)
                    .Include(x => x.FormaDePago)
                    .Where(x => x.UserId == UserId)
                    .FirstOrDefaultAsync();
        }
        [HttpGet("por-cliente/{UserId}")]
        public async Task<ActionResult<ResponseSuscripcion>> Get(string UserId)
        {
            ResponseSuscripcion response = new ResponseSuscripcion();

            var users = await this.context.Users.Where(x => x.Id == UserId).FirstOrDefaultAsync();

            var subs = await this.context.Suscripciones
                .Include(x => x.Categoria)
                .Include(x => x.FormaDePago)
                .Where(x => x.UserId == UserId)
                .OrderBy(x=> x.FechaEmision)
                .ToListAsync();

            response.Suscripciones = subs;
            response.User = users;

            return response;
        }

        [HttpGet("forma-pago")]
        public async Task<ActionResult<List<FormaDePago>>> GetFormasPago()
        {
            return await this.context.FormasDePagos.ToListAsync();
        }

        [HttpGet("cliente")]
        public async Task<ActionResult<List<Suscripcion>>> GetCliente()
        {
            return await this.context.Suscripciones.Include(x => x.Categoria)
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier) && !x.Pagado)
                .ToListAsync();
        }
        #endregion
        #region CUD
        [HttpPost]
        public async Task<ActionResult<ResponseSuscripcion>> Post(Suscripcion suscripcion)
        {
            try
            {
                if (suscripcion.Pagado)
                {
                    suscripcion.CategoriaId = CalcularCategoria(CantidadSilosUser(suscripcion.UserId));
                    suscripcion.Estado = Constants.PAGADO;
                    suscripcion.Total = CalcularTotal(suscripcion);
                }
                if (!suscripcion.Pagado)
                {
                    suscripcion.CategoriaId = CalcularCategoria(CantidadSilosUser(suscripcion.UserId));
                    suscripcion.FormaDePagoId = 1;
                    suscripcion.FechaPago = suscripcion.FechaEmision;
                    suscripcion.Id = GetNextIdSuscripcion();
                    suscripcion.Total = CalcularTotal(suscripcion);
                }
                context.Suscripciones.Add(suscripcion);
                await context.SaveChangesAsync();
                if (!suscripcion.Pagado)
                {
                    PrepararEnvioSolicitud(suscripcion);
                }
                return await Get(suscripcion.UserId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
        [HttpPost("anual")]
        public async Task<ActionResult> SuscripcionAnual(RequestUserId request)
        {
            Suscripcion sub;
            for (int i = 1; i <= 12; i++)
            {
                sub = await context.Suscripciones.Where(x => x.UserId == request.UserId && x.FechaEmision.Month == i && x.FechaEmision.Year == DateTime.Now.Year).FirstOrDefaultAsync();
                if (sub == null)
                {
                    Suscripcion suscripcion = new Suscripcion()
                    {
                        Id = GetNextIdSuscripcion(),
                        CategoriaId = CalcularCategoria(CantidadSilosUser(request.UserId)),
                        Estado = Constants.GENERADO,
                        FechaEmision = new DateTime(DateTime.Now.Year, i, 5),
                        FechaPago = new DateTime(DateTime.Now.Year, i, 5),
                        FormaDePagoId = Constants.FORMA_PAGO_EFECTIVO,
                        UserId = request.UserId,
                        Observaciones = "GENERADO AUTOMATICAMENTE",
                    };
                    suscripcion.Total = CalcularTotal(suscripcion);
                    context.Suscripciones.Add(suscripcion);
                    await context.SaveChangesAsync();
                }              
            }
            return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult> VerificarPago(Suscripcion suscripcion)
        {
            try
            {
                //Verifico el pago
                suscripcion.Verificado = true;
                //Verifico la solicitud
                var subSolicitud = context.Suscripciones.Where(x => x.Id == suscripcion.Id && x.Pagado == false).FirstOrDefault();
                subSolicitud.Verificado = true;

                context.Entry(suscripcion).State = EntityState.Modified;
                context.Entry(subSolicitud).State = EntityState.Modified;
                await context.SaveChangesAsync();
                PrepararEnvioPagoVerificado(suscripcion);
                return NoContent();
            }
            catch (Exception e)
            {

                throw new InvalidOperationException(e.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var suscripcion = new List<Suscripcion>() {
                new Suscripcion { Id = id, Pagado = false },
                new Suscripcion { Id = id, Pagado = true}
            };
            foreach (var item in suscripcion)
            {
                context.Remove(item);
                await context.SaveChangesAsync();
            }
            return NoContent();
        }
        #endregion
        #region PRIVADOS
        private int GetNextIdSuscripcion()
        {
            var sub = context.Suscripciones.OrderByDescending(x => x.Id).FirstOrDefault();
            if (sub == null)
            {
                return 0;
            }
            return (sub.Id + 1);
        }
        private int CantidadSilosUser(string userId)
        {
            var cantidadSilos = this.context.Silos
                .Where(x => x.Dispositivos.Count > 0)
                .Where(x => x.UserId == userId)
                .Count();
            return cantidadSilos;
        }
        private double CalcularTotal(Suscripcion suscripcion)
        {
            var categoria = context.Categorias.Where(x => x.Id == suscripcion.CategoriaId).FirstOrDefault();

            switch (categoria.Id)
            {
                case Constants.CATEGORIA_BASE:
                    strategy = new EstrategiaC();
                    break;
                case Constants.CATEGORIA_STANDAR:
                    strategy = new EstrategiaC();
                    break;
                case Constants.CATEGORIA_PRO:
                    strategy = new EstrategiaB();
                    break;
                case Constants.CATEGORIA_PREMIUM:
                    strategy = new EstrategiaA();
                    break;
                default:
                    break;
            }
            var total = strategy.Total(categoria.Costo);
            return total;
        }
        private int CalcularCategoria(int cantidadSilos)
        {
            if (cantidadSilos <= 5)
            {
                return Constants.CATEGORIA_BASE;
            }
            if (cantidadSilos <= 10)
            {
                return Constants.CATEGORIA_STANDAR;
            }
            if (cantidadSilos <= 19)
            {
                return Constants.CATEGORIA_PRO;
            }
            else
            {
                return Constants.CATEGORIA_PREMIUM;
            }
        }
        private void PrepararEnvioPagoVerificado(Suscripcion suscripcion)
        {
            var mailCampo = context.Users.Where(x => x.Id == suscripcion.UserId).FirstOrDefault();
            var sub = context.Suscripciones.Where(x => x.Id == suscripcion.Id).Include(x => x.Categoria).FirstOrDefault();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Sistema " + Constants.nombreEmpresa);
            sb.AppendLine("");
            sb.AppendLine("Estimado usuario, le hacemos llegar este correo ya que se verificado un pago que fue realizado el día " + suscripcion.FechaPago.ToString("dd/MM/yyyy"));
            sb.AppendLine("Gracias por seguir utilizando nuestro sistema.");
            sb.AppendLine("");
            sb.AppendLine("Servicio de Soporte " + Constants.nombreEmpresa);

            EnviarMail(mailCampo.Email, "SISTEMA " + Constants.nombreEmpresa.ToUpper(), sb.ToString());
        }
        private void PrepararEnvioSolicitud(Suscripcion suscripcion)
        {
            var mailCampo = context.Users.Where(x => x.Id == suscripcion.UserId).FirstOrDefault();
            var sub = context.Suscripciones.Where(x => x.Id == suscripcion.Id).Include(x => x.Categoria).FirstOrDefault();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Sistema " + Constants.nombreEmpresa);
            sb.AppendLine(" ");
            sb.Append("Estimado usuario, le hacemos llegar este correo ya que se ha generado una nueva solicitud de pago. ");
            sb.Append("Usted podrá seleccionar en nuestro sistema, cualquiera de los métodos de pago disponibles. ");
            sb.AppendLine("Allí encontrará la información necesaria para realizar el pago de su suscripción. ");
            sb.AppendLine("");
            sb.AppendLine("Fecha de emisión del pago: " + suscripcion.FechaEmision.ToString("dd/MM/yyyy"));
            sb.AppendLine("Saldo a pagar: $" + suscripcion.Total);
            sb.AppendLine("Categoria actual: " + suscripcion.Categoria.Descripcion);
            if (!string.IsNullOrEmpty(suscripcion.Observaciones))
            {
                sb.AppendLine("Observaciones de la solictud: " + suscripcion.Observaciones);
            }
            sb.AppendLine(" ");
            sb.AppendLine("Recuerde que posse 10 días hábiles para acreditar y notificar el mismo. De lo contrario, nos vemos en derecho de bloquear su cuenta. ");
            sb.AppendLine("");
            sb.AppendLine("Servicio de Soporte " + Constants.nombreEmpresa);

            EnviarMail(mailCampo.Email, "SISTEMA " + Constants.nombreEmpresa.ToUpper(), sb.ToString());
        }
        private bool EnviarMail(string para, string asunto, string cuerpo)
        {
            MailServiceController mail = new MailServiceController(context, _userManager);
            mail.SendMessage(para, asunto, cuerpo);
            return true;
        }

        #endregion

    }
}
