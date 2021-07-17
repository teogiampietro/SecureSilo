using System;
using System.ComponentModel.DataAnnotations;

namespace SecureSilo.Shared
{
    public class Suscripcion
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
        
    }
}
