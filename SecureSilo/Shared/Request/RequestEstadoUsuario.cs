using SecureSilo.Shared.Identity;

namespace SecureSilo.Shared
{
    public class RequestEstadoUsuario
    {
        public string UserId { get; set; }
        public bool Active { get; set; }
    }
}
