using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecureSilo.Shared
{
    public class Grano
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<Parametro> Parametros { get; set; }
    }
}
