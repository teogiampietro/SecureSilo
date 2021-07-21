﻿using System;
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
        public string Observaciones { get; set; }
        public FormaDePago FormaDePago { get; set; }
        public int FormaDePagoId { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }     
        public double Total { get; set; }
        
    }
}
