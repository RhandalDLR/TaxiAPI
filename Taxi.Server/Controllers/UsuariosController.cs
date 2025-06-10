using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taxi.Server.Models;

namespace Taxi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly TaxiContext _context;
        public UsuariosController(TaxiContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("CrearUsuario")]

        public async Task <IActionResult> CrearUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El usuario no puede ser nulo.");
            }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok(usuario);
        }

        [HttpGet]
        [Route("ObtenerUsuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound("No se encontraron usuarios.");
            }
            return Ok(usuarios);
        }

        [HttpGet]
        [Route("ObtenerUsuarioPorId/{id}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado.");
            }
            return Ok(usuario);
        }

        [HttpPut]
        [Route("ActualizarUsuario/{id}")]

        public async Task<IActionResult> ActualizarUsuario(int id, Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            usuarioExistente.Documento = usuario.Documento;
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;

            await _context.SaveChangesAsync();
            return Ok(usuarioExistente);

        }

        [HttpDelete]
        [Route("EliminarUsuario/{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado.");
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return Ok($"Usuario con ID {id} eliminado correctamente.");
        }
    }

}

