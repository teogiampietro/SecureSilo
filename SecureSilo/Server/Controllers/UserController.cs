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

        [HttpGet("users-name/{userName}")]
        public async Task<ActionResult<List<ResponseIndexSuscripcion>>> GetUsersxName(string userName)
        {
            return await (from users in context.Users
                          select new ResponseIndexSuscripcion()
                          {
                              UserId = users.Id,
                              UserName = users.UserName,
                              UserMail = users.Email
                          })
                          .Where(x=> x.UserName.Contains(userName))
                          .ToListAsync();
        }
    }
}
