﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SecureSilo.Shared;
using SecureSilo.Server.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using SecureSilo.Shared.Identity;
using SecureSilo.Server.Extensions;

namespace SecureSilo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DispositivosController : ControllerBase
    {

        public readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DispositivosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }
        #region CUD
        [HttpPost]
        public async Task<ActionResult<List<Dispositivo>>> Post(Dispositivo dispositivo)
        {
            Silo silo = new Silo();
            silo = this.context.Silos
                .Include(x => x.Dispositivos)
                .Where(x => x.Id == dispositivo.SiloId).FirstOrDefault();
            if (silo.Dispositivos != null && silo.Dispositivos.Count >= 10)
            {
                return new BadRequestResult();
            }
            if (string.IsNullOrEmpty(dispositivo.UserId))
            {
                dispositivo.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            context.Add(dispositivo);
            await context.SaveChangesAsync();
            if (String.IsNullOrEmpty(dispositivo.Descripcion))
            {
                return new CreatedAtRouteResult("obtenerDispositivo", new { id = dispositivo.Id, descripcion = ("dsp" + dispositivo.Id) }, dispositivo);
            }
            else
            {
                return new CreatedAtRouteResult("obtenerDispositivo", new { id = dispositivo.Id }, dispositivo);
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dispositivo = new Dispositivo { Id = id };
            context.Remove(dispositivo);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult> Put(Dispositivo dispositivo)
        {
            context.Entry(dispositivo).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        #endregion
        #region GET
        //Endpoint para traer todos los dispositivos
        [HttpGet]
        public async Task<ActionResult<List<Dispositivo>>> Get()
        {
            return await context.Dispositivos
                .Include(x => x.Silo)
                .Include(y => y.Silo.Campo)
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToListAsync();
        }
        //Endpoint para traer un dispositivos por id
        [HttpGet("{id}", Name = "obtenerDispositivo")]
        public async Task<ActionResult<Dispositivo>> Get(int id)
        {
            return await context.Dispositivos
                .Include(x => x.Silo.Campo)
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        //Endpoint para traer una lista de dispositivos que pertenecen a un id de silo
        [HttpGet("GetDispositivoPorSilo/{idSilo}&{alarma}")]
        public async Task<ActionResult<List<Dispositivo>>> GetDispositivosPorSilo(int idSilo, int alarma = 0)
        {
            return await context.Dispositivos
                .Include(x => x.Updates)
                .WhereIf(alarma == 1, x => x.Updates.Any(y => y.A == "FF"))
                .Where(x => x.SiloId == idSilo)
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToListAsync();
        }
        #endregion
    }
}
