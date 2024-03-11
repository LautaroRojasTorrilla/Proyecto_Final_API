using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_API.DTO;
using Proyecto_Final_API.Models;
using Proyecto_Final_API.Service;

namespace Proyecto_Final_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioController : Controller
    {
        private UsuarioService usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet("listado")]
        public IActionResult ObtenerListadoDeUsuarios()
        {
            try
            {
                List<Usuario> usuarios = this.usuarioService.ObtenerTodosLosUsuarios();

                if (usuarios != null && usuarios.Any())
                {
                    return Ok(usuarios);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el listado de usuarios: {ex.Message}");
            }
        }

        [HttpPost("crearUsuario")]
        public IActionResult AgregarUsuario([FromBody] UsuarioDTO usuario)
        {
            try
            {
                if (this.usuarioService.AgregarUsuario(usuario))
                {
                    return Ok(new { mensaje = "Se agregó el usuario", usuario });
                }
                else
                {
                    return Conflict(new { mensaje = "No se pudo agregar el usuario" });
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = "Error de validación", detalle = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al procesar la solicitud", detalle = ex.Message });
            }
        }

        [HttpPost("iniciarSesion")]
        public IActionResult IniciarSesion([FromBody] CredencialesDTO credenciales)
        {
            try
            {
                bool sesionIniciada = this.usuarioService.IniciarSesion(credenciales.NombreUsuario, credenciales.Contraseña);

                if (sesionIniciada)
                {
                    return Ok(new { mensaje = "Inicio de sesión exitoso" });
                }
                else
                {
                    return Unauthorized(new { mensaje = "Credenciales incorrectas" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al procesar la solicitud", detalle = ex.Message });
            }
        }
    }
}
