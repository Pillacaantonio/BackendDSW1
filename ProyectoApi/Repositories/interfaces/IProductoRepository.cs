using ProyectoApi.Models.Queries;
using ProyectoApi.Models.Request;
using ProyectoApi.Models.Response;

namespace ProyectoApi.Repositories.interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<ProductoResponse>> SearchProducto();
        Task<string> CrearProducto(ProductoRequest request);
        Task<ProductoOneResponse> GetOneProducto(int id_producto);
        Task<string> Update(int id_producto, ProductoRequest request);
    }
}
