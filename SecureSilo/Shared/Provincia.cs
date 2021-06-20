using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecureSilo.Shared
{
   public  class Provincia
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int PaisId { get; set; }
        public Pais Pais { get; set; }
    }
}
