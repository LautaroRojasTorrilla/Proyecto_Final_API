using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_API.DTO;
using Proyecto_Final_API.Models;
using Proyecto_Final_API.Service;

namespace Proyecto_Final_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : Controller
    {
        private VentaService ventaService;

        public VentaController(VentaService ventaService)
        {
            this.ventaService = ventaService;
        }

        [HttpGet("listado")]
        public IActionResult ObtenerTodasLasVentas()
        {
            try
            {
                List<Venta> ventas = ventaService.ObtenerTodasLosVentas();
                return Ok(ventas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener todas las ventas: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerVentaPorId(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(new { mensaje = "El ID de la venta debe ser mayor o igual a 1." });
                }

                Venta venta = ventaService.ObtenerVentaPorId(id);
                return Ok(venta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }
        }

        [HttpPost("{idUsuario}")]
        public IActionResult AgregarVenta(int idUsuario, List<ProductoDTO> productosDTO)
        {
            try
            {
                bool ventaAgregada = ventaService.AgregarVenta(idUsuario, productosDTO);
                if (ventaAgregada)
                {
                    return Ok(new { mensaje = "Venta agregada correctamente." });
                }
                else
                {
                    return StatusCode(500, new { mensaje = "Error al agregar la venta." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al agregar la venta: {ex.Message}" });
            }
        }
    }
}
