using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureSilo.Server.Data;
using SecureSilo.Shared;
using SecureSilo.Shared.Identity;
using System;
using System.Collections.Generic;
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
        [HttpPost]
        public async Task<ActionResult<ResponseSuscripcion>> Post(Suscripcion suscripcion)
        {
            try
            {
                if (suscripcion.Pagado)
                {
                    suscripcion.CategoriaId = CalcularCategoria(CantidadSilos(suscripcion.UserId));
                    suscripcion.Estado = Constants.PAGADO;
                }
                if (!suscripcion.Pagado)
                {
                    suscripcion.CategoriaId = CalcularCategoria(CantidadSilos(suscripcion.UserId));
                    suscripcion.FormaDePagoId = 1;
                    suscripcion.FechaPago = suscripcion.FechaEmision;
                    suscripcion.Id = GetNextId();
                }
                context.Suscripciones.Add(suscripcion);
                await context.SaveChangesAsync();
                if (!suscripcion.Pagado)
                {
                    EnviarMailAsync(suscripcion);
                }
                return await Get(suscripcion.UserId);
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
        #region PRIVADOS
        private int GetNextId()
        {
            var sub = context.Suscripciones.OrderByDescending(x=>x.Id).FirstOrDefault();
            if (sub == null)
            {
                return 0;
            }
            return ( sub.Id +  1);
        }
        private int CantidadSilos(string userId)
        {
            var cantidadSilos = this.context.Silos
                .Where(x => x.Dispositivos.Count > 0)
                .Where(x => x.UserId == userId)
                .Count();
            return cantidadSilos;
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

        private string ArmarBodyEmail(Suscripcion suscripcion)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Sistema Secure Silo");
            sb.Append("Estimado usuario, le hacemos llegar este correo porque se ha generado una nueva solicitud de pago");
            sb.Append("Usted podrá seleccionar en nuestro sistema, cualquiera de los métodos de pago disponibles.");
            sb.AppendLine("Allí encontrará la información necesaria para realizar el pago de su suscripción.");
            sb.AppendLine("");
            sb.AppendLine("Fehca de emisión del pago:" + suscripcion.FechaEmision.ToString("dd/MM/yyyy"));
            sb.AppendLine("Saldo a pagar: " + suscripcion.Categoria.Costo);
            sb.AppendLine("Categoria actual: " + suscripcion.Categoria.Descripcion);
            if (!string.IsNullOrEmpty(suscripcion.Observaciones))
            {
                sb.AppendLine("Observaciones del pago " + suscripcion.Observaciones);
            }
            sb.AppendLine(" ");
            sb.AppendLine("Sistema de monitoreo Secure Silo");
            return string.Empty;
        }
        private void EnviarMailAsync(Suscripcion suscripcion)
        {
            MailServiceController mail = new MailServiceController(context, _userManager);
            //capturar mail según usuario
            var mailCampo = context.Users.Where(x => x.Id == suscripcion.UserId).FirstOrDefault();

            var resultado = mail.SendMessage(mailCampo.Email, "SISTEMA SECURE SILO", ArmarBodyEmail(suscripcion));
        }
        #endregion

    }
}
