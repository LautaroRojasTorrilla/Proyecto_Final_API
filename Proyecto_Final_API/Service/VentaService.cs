using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Proyecto_Final_API.Database;
using Proyecto_Final_API.DTO;
using Proyecto_Final_API.Models;
using System.Runtime.CompilerServices;

namespace Proyecto_Final_API.Service
{
    public class VentaService
    {
        private CoderContext context;
        private ProductoVendidoService productoVendidoService;
        private ProductoService productoService;

        public VentaService(CoderContext coderContext, ProductoVendidoService productoVendidoService,
            ProductoService productoService)
        {
            this.context = coderContext;
            this.productoVendidoService = productoVendidoService;
            this.productoService = productoService;
        }

        public List<Venta> ObtenerTodasLosVentas()
        {
            try
            {
                return this.context.Venta.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener todas las ventas: {ex.Message}", ex);
            }
        }

        public Venta ObtenerVentaPorId(int id)
        {
            try
            {
                Venta? ventaBuscada = context.Venta.FirstOrDefault(v => v.Id == id)
                    ?? throw new Exception($"No se encontró una venta con ID {id}");

                return ventaBuscada;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la venta por ID: {ex.Message}", ex);
            }
        }

        public bool AgregarVenta(int idUsuario, List<ProductoDTO> productosDTO)
        {
            try
            {
                Venta venta = new Venta();

                List<string> nombresProductos = productosDTO.Select(p => p.Descripciones).ToList();
                string comentarios = string.Join("-", nombresProductos);
                venta.Comentarios = comentarios;
                venta.IdUsuario = idUsuario;

                EntityEntry<Venta>? resultado = context.Venta.Add(venta);
                resultado.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                context.SaveChanges();

                RegistrarProductosComoVendidos(productosDTO, venta.Id);
                ActualizarStock(productosDTO);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar la venta: {ex.Message}", ex);
            }
        }

        private void RegistrarProductosComoVendidos(List<ProductoDTO> productosDTO, int idVenta)
        {
            productosDTO.ForEach(p =>
            {
                ProductoVendidoDTO productoVendidoDTO = new ProductoVendidoDTO();
                productoVendidoDTO.IdProducto = p.Id;
                productoVendidoDTO.IdVenta = idVenta;
                productoVendidoDTO.Stock = p.Stock;

                productoVendidoService.AgregarProductoVendido(productoVendidoDTO);
            });
        }

        private void ActualizarStock(List<ProductoDTO> productosDTO)
        {
            productosDTO.ForEach(productoDTO =>
            {
                Producto? producto = this.context.Productos.FirstOrDefault(p => p.Id == productoDTO.Id);

                if (producto != null)
                {
                    producto.Stock -= productoDTO.Stock;

                    this.context.SaveChanges();
                }
            });
        }
    }
}
