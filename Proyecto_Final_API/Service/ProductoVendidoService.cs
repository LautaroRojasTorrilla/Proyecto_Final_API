using Microsoft.EntityFrameworkCore;
using Proyecto_Final_API.Database;
using Proyecto_Final_API.DTO;
using Proyecto_Final_API.Mapper;
using Proyecto_Final_API.Models;
using System.Data.Common;

namespace Proyecto_Final_API.Service
{
    public class ProductoVendidoService
    {

        private CoderContext context;

        public ProductoVendidoService(CoderContext coderContext)
        {
            this.context = coderContext;
        }

        public List<ProductoVendido> ObtenerTodosLosProductosVendidos()
        {
            try
            {
                return this.context.ProductoVendidos.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener todos los productos: {ex.Message}", ex);
            }
        }
        
        public List<ProductoVendidoDTO> ObtenerProductoVendidoPorIDUsuario(int idUsuario)
        {
            try
            {

                bool usuarioExiste = this.context.Usuarios.Any(u => u.Id == idUsuario);
                if (!usuarioExiste)
                {
                    throw new ArgumentException("No existe el usuario ingresado.");
                }

                List<ProductoVendido>? productoVendidos = this.context.Productos
                    .Include(p => p.ProductoVendidos)
                    .Where(p => p.IdUsuario == idUsuario)
                    .SelectMany(p => p.ProductoVendidos)
                    .ToList();

                List<ProductoVendidoDTO> dto = productoVendidos
                    .Where(pv => pv != null)
                    .Select(pv => ProductoVendidoMapper.MapearADTO(pv))
                    .ToList();

                return dto;
            }
            catch (DbException ex)
            {
                throw new Exception($"Error al obtener los productos vendidos por ID de usuario: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al obtener los productos vendidos por ID de usuario: {ex.Message}", ex);
            }
        }

        public bool AgregarProductoVendido(ProductoVendidoDTO dto)
        {
            try
            {
                if (dto.Stock <= 0)
                {
                    throw new ArgumentException("El stock debe ser mayor a 0.");
                }

                ProductoVendido p = ProductoVendidoMapper.MapearAProductoVendido(dto);

                this.context.ProductoVendidos.Add(p);
                context.SaveChanges();
                return true;

            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error al agregar el producto: {ex.Message}", ex);
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Error de validación: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
