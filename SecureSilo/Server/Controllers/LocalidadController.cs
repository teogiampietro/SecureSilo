using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SecureSilo.Shared;
using SecureSilo.Server.Data;

namespace SecureSilo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocalidadController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        public LocalidadController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Localidad>>> GetLocalidades(int provinciaId)
        {
            return await context.Localidades.Where(x =>x.Provincia.Id == provinciaId).ToListAsync();
        }
        [HttpGet("provincias")]
        public async Task<ActionResult<List<Provincia>>> GetProvincias()
        {
            return await context.Provincias.ToListAsync();
        }
    }
}
