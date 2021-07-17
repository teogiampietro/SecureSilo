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
        public double HumedadMinValue { get; set; }
        public double HumedadMaxValue { get; set; }
        public double TemperaturaValue { get; set; }
        public int GranoID { get; set; }
        public Grano Grano { get; set; }
    }
}
