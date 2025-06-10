using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taxi.Server.Models;

namespace Taxi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiController : ControllerBase
    {
        private readonly TaxiContext _context;
        public TaxiController(TaxiContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AgregarVehiculo")]
        public async Task<IActionResult> AgregarVehiculo(Models.Vehiculo vehiculo)
        {
            if (vehiculo == null)
            {
                return BadRequest("El Taxi no puede ser nulo");
            }
            _context.Taxi.Add(vehiculo);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        [Route("ObtenerVehiculo")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> ObtenerVehiculo()
        {
            var vehiculo = await _context.Taxi.ToListAsync();
            if (vehiculo == null || !vehiculo.Any())
            {
                return NotFound("No se encontraron Vehiculos");
            }
            return Ok(vehiculo);
        }



        [HttpGet]
        [Route("ObtenerVehiculoPorID/{id}")]
        public async Task <ActionResult<Vehiculo>> ObtenerVehiculoPorId(int id)
        {
            var vehiculo = await _context.Taxi.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound($"Vehiculo con ID {id} no encontrado.");
            }
            return Ok(vehiculo);
        }

        [HttpPut]
        [Route ("ActualizarVehiculo")]
        public async Task <IActionResult> ActualizarVehiculo(Vehiculo vehiculo, int Id)
        {
            var VehiculoExistente = await _context.Taxi.FindAsync(Id);

            VehiculoExistente.Placa = vehiculo.Placa;

            await _context.SaveChangesAsync();
            return Ok(VehiculoExistente);
        }

        [HttpDelete]
        [Route("EliminarVehiculo/{id}")]
        public async Task <IActionResult> EliminarVehiculo(int id)
        {
            var vehiculo =  await _context.Taxi.FindAsync(id);
            if(vehiculo == null)
            {
                return NotFound($"Vehiculo con ID {id} no encontrado.");
            }
            _context.Taxi.Remove(vehiculo);
            return Ok($"Usuario con ID {id} eliminado correctamente.");

        }
    }
}

