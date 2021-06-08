using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureSilo.Shared
{
    public class Parametro
    {
        [Key]
        public int Id { get; set; }
        public string Riesgo { get; set; }
        public double HumedadValue { get; set; }
        public double TemperaturaValue { get; set; }
        public double CO2Value { get; set; }
    }
}
