using Proyecto_Final_API.Database;
using Proyecto_Final_API.Models;

namespace Proyecto_Final_API.Service
{
    public class VentaService
    {
        public static List<Venta> ObtenerTodasLasVentas()
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    List<Venta> ventas = contexto.Venta.ToList();
                    return ventas;
                }
            }
            catch (Exception ex)
            {
                // Lanzar una nueva excepción para manejar el error en un nivel superior.
                throw new Exception($"Error al obtener todas las ventas: {ex.Message}", ex);
            }
        }

        public static Venta ObtenerVentaPorId(int id)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    Venta ventaBuscada = contexto.Venta.FirstOrDefault(v => v.Id == id)
                        ?? throw new Exception($"No se encontró una venta con ID {id}");

                    return ventaBuscada;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la venta por ID: {ex.Message}", ex);
            }
        }

        public static bool AgregarVenta(Venta venta)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    contexto.Venta.Add(venta);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar la venta: {ex.Message}", ex);
            }
        }

        public static bool ModificarVente(Venta venta, int id)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    Venta ventaBuscada = contexto.Venta.FirstOrDefault(v => v.Id == id)
                        ?? throw new Exception($"No se encontró la venta con ID {id}");

                    ventaBuscada.Comentarios = venta.Comentarios;

                    contexto.Venta.Update(ventaBuscada);

                    contexto.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static bool EliminarVentaPorID(int Id)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    Venta ventaAEliminar = contexto.Venta.FirstOrDefault(v => v.Id == Id)
                        ?? throw new Exception($"No se encontró una venta con ID {Id}");

                    contexto.Venta.Remove(ventaAEliminar);
                    contexto.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la venta: {ex.Message}", ex);
            }
        }
    }
}
