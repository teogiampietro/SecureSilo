using System.ComponentModel.DataAnnotations;

namespace SecureSilo.Shared
{
    public class RequestContacto
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Telefono { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
    }
}
