using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_API.DTO;
using Proyecto_Final_API.Models;
using Proyecto_Final_API.Service;

namespace Proyecto_Final_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private ProductoService productoService;

        public ProductoController(ProductoService productoService)
        {
            this.productoService = productoService;
        }

        [HttpGet("listado")]
        public IActionResult ObtenerListadoDeProductos()
        {
            try
            {
                List<Producto> productos = this.productoService.ObtenerTodosLosProductos();

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
                return StatusCode(500, $"Error al obtener el listado de productos: {ex.Message}");
            }
        }


        [HttpPost("crearProducto")]
        public IActionResult AgregarProducto([FromBody] ProductoDTO producto)
        {
            try
            {
                if (this.productoService.AgregarProducto(producto))
                {
                    return Ok(new { mensaje = "Se agregó el producto", producto });
                }
                else
                {
                    return Conflict(new { mensaje = "No se pudo agregar el producto" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
        }

        [HttpPut("{id}")]

        public IActionResult ModificarProducto(int id, ProductoDTO producto)
        {
            try
            {
                if (id >= 1)
                {
                    if (this.productoService.ModificarProductoPorId(id, producto))
                    {
                        return Ok(new { mensaje = "Producto actualizado", status = 200, producto });
                    }
                    return Conflict(new { mensaje = "No se pudo actualizar el producto" });
                }
                return BadRequest(new { status = 400, mensaje = "El ID no puede ser menor a 1" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]

        public IActionResult BorrarProducto(int id)
        {
            try
            {
                if (id > 0)
                {
                    bool productoEliminado = this.productoService.EliminarProductoPorID(id);

                    if (productoEliminado)
                    {
                        return Ok(new { mensaje = "Producto eliminado", status = 200 });
                    }

                    return Conflict(new { mensaje = "No se pudo borrar el producto. El producto no existe.", status = 409 });
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
