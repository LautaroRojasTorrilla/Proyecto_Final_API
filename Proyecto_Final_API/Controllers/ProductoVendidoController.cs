using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_API.DTO;
using Proyecto_Final_API.Models;
using Proyecto_Final_API.Service;

namespace Proyecto_Final_API.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
    public class ProductoVendidoController : Controller
    {

        private ProductoVendidoService productoVendidoService;

        public ProductoVendidoController(ProductoVendidoService productoVendidoService)
        {
            this.productoVendidoService = productoVendidoService;
        }


        [HttpGet("listado")]
        public IActionResult ObtenerListadoDeProductosVendidos()
        {
            try
            {
                List<ProductoVendido> productos = this.productoVendidoService.ObtenerTodosLosProductosVendidos();

                if (productos != null && productos.Any())
                {
                    return Ok(productos);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el listado de productos vendidos: {ex.Message}");
            }
        }

        [HttpGet("{idUsuario}")]

        public IActionResult ObtenerListadoDeProductosVendidosIdUSuario(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    return BadRequest(new { mensaje = "El ID de usuario debe ser mayor que cero." });
                }

                List<ProductoVendidoDTO> productos = this.productoVendidoService.ObtenerProductoVendidoPorIDUsuario(idUsuario);

                if (productos != null && productos.Any())
                {
                    return Ok(productos);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }
        }
    }
}
