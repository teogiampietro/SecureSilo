using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SecureSilo.Shared;
using SecureSilo.Server.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SecureSilo.Shared.Identity;

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

        [HttpPost]
        public async Task<ActionResult<ResponseSuscripcion>> Post(Suscripcion suscripcion)
        {
            context.Suscripciones.Add(suscripcion);
            await context.SaveChangesAsync();
            return await Get(suscripcion.UserId);
        }

        [HttpGet("forma-pago")]
        public async Task<ActionResult<List<FormaDePago>>> GetFormasPago()
        {
            return await this.context.FormasDePagos.ToListAsync();
        }
    }
}
