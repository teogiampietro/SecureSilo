using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace SecureSilo.Server.Helpers
{
    public class ServiciousuarioActual : IServicioUsuarioActual
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public ServiciousuarioActual(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException();
        }
        public string ObtenerIdUsuarioActual()
        {
            return httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
