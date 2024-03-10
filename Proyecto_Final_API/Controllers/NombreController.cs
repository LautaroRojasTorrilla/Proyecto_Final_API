using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NombreController : Controller
    {
        List<string> list = new List<string>() { "Lautaro", "Pepe", "Juan" };

        [HttpGet]
        public string ObtenerNombre()
        {
            return "Lautaro Rojas Torrilla";
        }

        [HttpGet("listado")]
        public List<string> ObtenerListaDeNombres()
        {
            return this.list;
        }

        [HttpGet("listado/{id}")]
        public ActionResult<string> ObtenerNombrePorId(int id)
        {
            if (id < 0 || id >= list.Count)
            {
                return BadRequest(new { mensaje = "Numero no válido", status = 400 });
            }

            return this.list[id];
        }
    }
}
