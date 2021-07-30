using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureSilo.Server.Data;
using SecureSilo.Shared;
using SecureSilo.Shared.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecureSilo.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        public readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<ResponseUserRol>>> GetRoles()
        {
            List<ResponseUserRol> roles = new List<ResponseUserRol>();
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(user))
            {
                roles = await (from userRoles in context.UserRoles
                               join rol in context.Roles on userRoles.RoleId equals rol.Id
                               where userRoles.UserId == user
                               select new ResponseUserRol()
                               {
                                   RolName = rol.Name,
                                   User = userRoles.UserId
                               }).ToListAsync();
            }
            return roles;

        }

        [HttpGet("users")]
        public async Task<ActionResult<List<ResponseIndexSuscripcion>>> GetUsers()
        {
            return await (from users in context.Users
                          select new ResponseIndexSuscripcion()
                          {
                              UserId = users.Id,
                              UserName = users.UserName,
                              UserMail = users.Email
                          })
                          .ToListAsync();
        }

        [HttpGet("users-name/{UserName}")]
        public async Task<ActionResult<List<ResponseIndexSuscripcion>>> GetUsersxName(string UserName)
        {
            return await (from users in context.Users
                          select new ResponseIndexSuscripcion()
                          {
                              UserId = users.Id,
                              UserName = users.UserName,
                              UserMail = users.Email
                          })
                          .Where(x => x.UserName.Contains(UserName))
                          .ToListAsync();
        }

        [HttpGet("user-active")]
        public async Task<bool> GetUserState(string email)
        {
            var response = await context.Users.Where(x => x.Email == email).Select(x => x.Active).FirstOrDefaultAsync();
            return response;
        }
        [HttpPost("estado")]
        public async Task<ActionResult<bool>> ActivarDesactivarUser(RequestEstadoUsuario user)
        {
            var appUser = await _userManager.FindByIdAsync(user.UserId);
            appUser.Active = user.Active;
            context.Users.Update(appUser);
            await context.SaveChangesAsync();
            return true;
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<ResponseUsuario>> GetUserById(string UserId)
        {
            var user = await (from users in context.Users
                              where users.Id == UserId
                              select new ResponseUsuario()
                              {
                                  Id = users.Id,
                                  Name = users.UserName,
                                  Email = users.Email,
                                  Active = users.Active
                              }).FirstOrDefaultAsync();

            return user;
        }
        [HttpPost("consulta")]
        public Task<bool> PostConsulta(RequestContacto contacto)
        {
            MailServiceController mail = new MailServiceController(context, _userManager);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Sistema Secure Silo");
            sb.AppendLine(" ");
            sb.AppendLine(contacto.Nombre);
            sb.AppendLine(contacto.CorreoElectronico);
            sb.AppendLine(contacto.Telefono);
            sb.AppendLine(contacto.Asunto);
            sb.AppendLine(contacto.Mensaje);
            sb.AppendLine(" ");

            return Task.FromResult(mail.SendMessage("securesilo@gmail.com", "Nueva Consulta", sb.ToString()));          
        }
    }
}
