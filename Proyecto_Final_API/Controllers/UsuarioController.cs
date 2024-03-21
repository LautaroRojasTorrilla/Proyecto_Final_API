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

        [HttpGet("{id}")]
        public IActionResult ObtenerUsuario(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(new { mensaje = "El ID del usuario debe ser mayor o igual a 1", status = 400 });
                }

                Usuario usuario = this.usuarioService.ObtenerUsuarioporID(id);

                if (usuario != null)
                {
                    return Ok(new { mensaje = "Usuario encontrado", status = 200, usuario });
                }
                else
                {
                    return NotFound(new { mensaje = "No se encontró el usuario", status = 404 });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
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

        [HttpPut("{id}")]
        public IActionResult ModificarUsuario(int id, UsuarioDTO usuario)
        {
            try
            {
                if (id >= 1)
                {
                    if (this.usuarioService.ModificarUsuarioPorId(id, usuario))
                    {
                        return Ok(new { mensaje = "Usuario actualizado", status = 200, usuario });
                    }
                    return Conflict(new { mensaje = "No se pudo actualizar el usuario" });
                }
                return BadRequest(new { status = 400, mensaje = "El ID no puede ser menor a 1" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al procesar la solicitud", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult BorrarUsuario(int id)
        {
            try
            {
                if (id > 0)
                {
                    bool usuarioEliminado = this.usuarioService.EliminarUsuarioPorID(id);

                    if (usuarioEliminado)
                    {
                        return Ok(new { mensaje = "Usuario eliminado", status = 200 });
                    }

                }

                return BadRequest(new { status = 400, mensaje = "El ID no puede ser menor o igual a 0" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al procesar la solicitud.", detalle = ex.Message });
            }
        }
    }
}
