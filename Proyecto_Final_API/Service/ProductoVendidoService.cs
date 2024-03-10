using Proyecto_Final_API.Database;
using Proyecto_Final_API.Models;

namespace Proyecto_Final_API.Service
{
    public static class ProductoVendidoService
    {
        public static List<ProductoVendido> ObtenerTodosLosProductosVendidos()
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    List<ProductoVendido> productosV = contexto.ProductoVendidos.ToList();
                    return productosV;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los productos: {ex.Message}");
                return new List<ProductoVendido>();
            }
        }

        public static ProductoVendido ObtenerProductoVendidoPorID(int id)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    ProductoVendido productoVBuscado = contexto.ProductoVendidos.FirstOrDefault(p => p.Id == id)
                        ?? throw new Exception($"No se encontró un producto vendido con ID {id}");

                    return productoVBuscado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el producto vendido por ID: {ex.Message}", ex);
            }
        }

        public static bool AgregarProductoVendido(ProductoVendido productoVendido)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    contexto.ProductoVendidos.Add(productoVendido);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el producto: {ex.Message}", ex);
            }
        }

        public static bool ModificarProductoVendidoPorId(ProductoVendido productoVendido, int id)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    ProductoVendido productoVBuscado = contexto.ProductoVendidos.FirstOrDefault(p => p.Id == id)
                        ?? throw new Exception($"No se encontró un producto vendido con ID {id}");

                    productoVBuscado.Stock = productoVendido.Stock;

                    contexto.ProductoVendidos.Update(productoVBuscado);
                    contexto.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al modificar el producto vendido: {ex.Message}", ex);
            }
        }

        public static bool EliminarProductoVendidoPorID(int Id)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    ProductoVendido productoVAEliminar = contexto.ProductoVendidos.FirstOrDefault(p => p.Id == Id)
                        ?? throw new Exception($"No se encontró un producto vendido con ID {Id}");

                    contexto.ProductoVendidos.Remove(productoVAEliminar);
                    contexto.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el producto vendido: {ex.Message}", ex);
            }
        }
    }
}
