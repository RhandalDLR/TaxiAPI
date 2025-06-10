using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taxi.Server.Models;

namespace Taxi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoUsuariosController : ControllerBase
    {
        private readonly TaxiContext _context;

        public GrupoUsuariosController(TaxiContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AgregarGrupo")]
        public async Task<IActionResult> AgregarGrupo(GrupoUsuarios grupo)
        {
            if (grupo == null)
            {
                return BadRequest("EL Grupo no puede ser nulo");
            }
            _context.GrupoUsuarios.Add(grupo);
            await _context.SaveChangesAsync();
            return Ok("Grupo Agregado Con Exito");

        }
        [HttpGet]
        [Route("ObtenerGrupos")]

        public async Task<ActionResult<IEnumerable<GrupoUsuarios>>>ObtenerGrupo()
        {
           var Grupos =await _context.GrupoUsuarios.ToListAsync();

            if ((Grupos == null || !Grupos.Any()))
            {
                return NotFound("No se encontraron Grupos de Usuarios");
            }
            return Ok(Grupos);
        }

        [HttpGet]
        [Route("ObtenerGrupoPorID/{id}")]
        public async Task<IActionResult> ObtenerGrupoPorId(int id)
        {
            var grupo = await _context.GrupoUsuarios.FindAsync(id);
            if (grupo == null)
            {
                return NotFound($"Grupo con ID {id} no encontrado.");
            }
            return Ok(grupo);
        }

        [HttpPut]
        [Route("ActualizarGrupo")]
        public async Task<IActionResult> ActualizarGrupo(GrupoUsuarios grupo, int id)
        {
            var grupoExistente = await _context.GrupoUsuarios.FindAsync(id);
            if (grupoExistente == null)
            {
                return NotFound($"Grupo con ID {id} no encontrado.");
            }
            grupoExistente.NombreGrupo = grupo.NombreGrupo;
           
            _context.GrupoUsuarios.Update(grupoExistente);
            await _context.SaveChangesAsync();
            return Ok("Grupo actualizado con éxito.");
        }
        [HttpDelete]
        [Route("EliminarGrupo/{id}")]
        public async Task<IActionResult> EliminarGrupo(int id)
        {
            var grupo = await _context.GrupoUsuarios.FindAsync(id);
            if (grupo == null)
            {
                return NotFound($"Grupo con ID {id} no encontrado.");
            }
            _context.GrupoUsuarios.Remove(grupo);
            await _context.SaveChangesAsync();
            return Ok("Grupo eliminado con éxito.");
        }
    }
}
