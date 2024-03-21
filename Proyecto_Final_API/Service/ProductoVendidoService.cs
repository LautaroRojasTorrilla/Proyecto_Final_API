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

                // Obtener la lista de productos vendidos por el usuario
                List<ProductoVendido>? productoVendidos = this.context.Productos
                    .Include(p => p.ProductoVendidos)
                    .Where(p => p.IdUsuario == idUsuario)
                    .SelectMany(p => p.ProductoVendidos)
                    .ToList();

                // Filtrar los productos vendidos que no sean nulos
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

        //public static bool AgregarProductoVendido(ProductoVendido productoVendido)
        //{
        //    try
        //    {
        //        using (CoderContext contexto = new CoderContext())
        //        {
        //            contexto.ProductoVendidos.Add(productoVendido);
        //            contexto.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error al agregar el producto: {ex.Message}", ex);
        //    }
        //}

        //public static bool ModificarProductoVendidoPorId(ProductoVendido productoVendido, int id)
        //{
        //    try
        //    {
        //        using (CoderContext contexto = new CoderContext())
        //        {
        //            ProductoVendido productoVBuscado = contexto.ProductoVendidos.FirstOrDefault(p => p.Id == id)
        //                ?? throw new Exception($"No se encontró un producto vendido con ID {id}");

        //            productoVBuscado.Stock = productoVendido.Stock;

        //            contexto.ProductoVendidos.Update(productoVBuscado);
        //            contexto.SaveChanges();

        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error al modificar el producto vendido: {ex.Message}", ex);
        //    }
        //}

        //public static bool EliminarProductoVendidoPorID(int Id)
        //{
        //    try
        //    {
        //        using (CoderContext contexto = new CoderContext())
        //        {
        //            ProductoVendido productoVAEliminar = contexto.ProductoVendidos.FirstOrDefault(p => p.Id == Id)
        //                ?? throw new Exception($"No se encontró un producto vendido con ID {Id}");

        //            contexto.ProductoVendidos.Remove(productoVAEliminar);
        //            contexto.SaveChanges();

        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error al eliminar el producto vendido: {ex.Message}", ex);
        //    }
        //}
    }
}
