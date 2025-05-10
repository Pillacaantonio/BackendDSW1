using ProyectoApi.Models.Queries;
using ProyectoApi.Models.Request;
using ProyectoApi.Models.Response;

namespace ProyectoApi.Repositories.interfaces
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<FacturaResponse>> GetFacturas();
        Task<string> CrearFactura(FacturaRequest request);
        Task<FacturaOneResponse> GetOneFactura(int idFactura, DateTime fecha, int clienteId);
        Task<string> UpdateEstado(int idFactura, string nuevoEstado, int cantidad);
    }
}
