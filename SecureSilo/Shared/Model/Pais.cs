using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecureSilo.Shared
{
    public class Pais
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
