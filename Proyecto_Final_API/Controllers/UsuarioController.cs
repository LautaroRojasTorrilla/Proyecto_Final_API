using Microsoft.AspNetCore.Mvc;
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
    }
}
