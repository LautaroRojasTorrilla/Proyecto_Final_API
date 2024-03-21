using Proyecto_Final_API.DTO;
using Proyecto_Final_API.Models;

namespace Proyecto_Final_API.Mapper
{
    public static class ProductoVendidoMapper
    {
        public static ProductoVendido MapearAProductoVendido(ProductoVendidoDTO dto)
        {
            ProductoVendido p = new ProductoVendido();

            p.Id = dto.Id;
            p.Stock = dto.Stock;
            p.IdProducto = dto.IdProducto;
            p.IdVenta = dto.IdVenta;

            return p;
        }

        public static ProductoVendidoDTO MapearADTO(ProductoVendido p)
        {
            ProductoVendidoDTO dto = new ProductoVendidoDTO();

            dto.Id = p.Id;
            dto.Stock = p.Stock;
            dto.IdProducto = p.IdProducto;
            dto.IdVenta = p.IdVenta;

            return dto;
        }
    }
}
