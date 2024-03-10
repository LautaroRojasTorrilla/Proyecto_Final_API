using Proyecto_Final_API.DTO;
using Proyecto_Final_API.Models;

namespace Proyecto_Final_API.Mapper
{
    public static class ProductoMapper
    {
        public static Producto MapearAProducto(ProductoDTO dto)
        {
            Producto p = new Producto();

            p.Id = dto.Id;
            p.Descripciones = dto.Descripciones;
            p.PrecioVenta = dto.PrecioVenta;
            p.IdUsuario = dto.IdUsuario;
            p.Stock = dto.Stock;
            p.Costo = dto.Costo;

            return p;
        }

        public static ProductoDTO MapearADTO(Producto p)
        { 
            ProductoDTO dto = new ProductoDTO();

            dto.Id = p.Id;
            dto.Descripciones = p.Descripciones;
            dto.PrecioVenta = p.PrecioVenta;
            dto.IdUsuario = p.IdUsuario;
            dto.Stock = p.Stock;
            dto.Costo = p.Costo;

            return dto;
        }
    }
}
