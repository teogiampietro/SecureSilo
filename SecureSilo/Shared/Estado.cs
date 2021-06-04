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
        struct Estados
        {
            public string Correcto => "Correcto";
            public string SinDatos => "SinDatos";
            public string Advertencia => "Advertencia";
            public string Alerta => "Alerta";

        }
    }
}
