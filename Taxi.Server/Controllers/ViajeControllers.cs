using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taxi.Server.Models;

namespace Taxi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajeControllers : ControllerBase
    {
        private readonly TaxiContext _context;

        public ViajeControllers(TaxiContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("AgregarViaje")]
        public async Task<IActionResult> AgregarViaje(Viaje viaje)
        {
            if (viaje == null)
            {
                return BadRequest("El viaje no puede ser nulo");

            }
            _context.Viajes.Add(viaje);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpGet]
        [Route("ObtenerViajes")]
        public async Task<ActionResult<IEnumerable<Viaje>>> ObtenerViajes()
        {
            var viajes = await _context.Viajes
                .Include(v => v.Taxi)       // Incluye los datos del taxi
                .Include(v => v.Usuario)    // Incluye los datos del usuario
                .ToListAsync();

            if (viajes == null || !viajes.Any())
            {
                return NotFound("No se encontraron Viajes...");
            }

            return Ok(viajes);
        }

        [HttpGet]
        [Route("ObtenerViajePorID/{id}")]
        public async Task<ActionResult<Viaje>> ObtenerViajePorID(int id)
        {
            var viaje = await _context.Viajes
                .Include(v => v.Taxi)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(v => v.id == id);
            if (viaje == null)
            {
                return NotFound($"VIaje con ID {id} no encontrado.");
            }
            return Ok(viaje);
        }

        [HttpPut]
        [Route("ActualizarViaje/{id}")]

        public async Task<IActionResult> ActualizarViaje(Viaje viaje, int id)
        {


            var viajeExistente = await _context.Viajes.FindAsync(id);
            if (viajeExistente == null)
            {
                return NotFound("Viaje con ID {id} no encontrado");
            }
            viajeExistente.FechaInicio = viaje.FechaInicio;
            viajeExistente.FechaFin = viaje.FechaFin;
            viajeExistente.Origen = viaje.Origen;
            viajeExistente.Destino = viaje.Destino;
            viajeExistente.Calificacion = viaje.Calificacion;

            await _context.SaveChangesAsync();
            return Ok(viajeExistente);

        }

        [HttpDelete]
        [Route("EliminarViaje/{id}")]

        public async Task<IActionResult> EliminarViaje(int id)
        {
            var viaje = await _context.Viajes.FindAsync(id);

            if (viaje == null)
            {
                return NotFound("Viaje con ID {id} no encontrado");
            }
            _context.Viajes.Remove(viaje);
            await _context.SaveChangesAsync();
            return Ok($"Viaje con ID {id} eliminado correctamente.");

        }
    }
}
