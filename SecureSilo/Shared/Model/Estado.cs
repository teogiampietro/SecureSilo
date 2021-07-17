using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureSilo.Shared
{
    public class Estado
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Riesgo { get; set; }
    }
}
