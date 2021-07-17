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
    public class CategoriasController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        public CategoriasController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<List<Categoria>> Categorias()
        {
            return await this.context.Categorias.ToListAsync();
        }
    }
}
