using ProyectoApi.Models.Request;

namespace ProyectoApi.Repositories.interfaces
{
    public interface IPedidosRepository
    {
        public Task<int> CrearPedido(PedidoRequest request);
    }
}
