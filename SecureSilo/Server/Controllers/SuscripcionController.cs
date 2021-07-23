using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureSilo.Server.Data;
using SecureSilo.Shared;
using SecureSilo.Shared.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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


        [HttpPost("solicitud-pago")]
        public async Task<ActionResult<ResponseSuscripcion>> Post(Suscripcion suscripcion)
        {
            if (suscripcion.Pagado)
            {
                suscripcion.FechaPago = System.DateTime.Today;
            }
            if (!suscripcion.Pagado)
            {
                suscripcion.CategoriaId = CalcularCategoria(CantidadSilos(suscripcion.UserId));
                suscripcion.FormaDePagoId = 1;
                suscripcion.FechaPago = suscripcion.FechaEmision;
            }
           

            context.Suscripciones.Add(suscripcion);
            await context.SaveChangesAsync();
            return await Get(suscripcion.UserId);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseSuscripcion>> Pagada(Suscripcion suscripcion)
        {
            context.Entry(suscripcion).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var suscripcion = new Suscripcion { Id = id };
            context.Remove(suscripcion);
            await context.SaveChangesAsync();
            return NoContent();
        }
        #region PRIVADOS

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
        #endregion

    }
}
