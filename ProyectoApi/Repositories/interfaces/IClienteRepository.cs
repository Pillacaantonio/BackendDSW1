using ProyectoApi.Models.Queries;
using ProyectoApi.Models.Request;
using ProyectoApi.Models.Response;

namespace ProyectoApi.Repositories.interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteResponse>> SearchClientes(ClienteQuery query);
        Task<string> CrearCliente(ClienteRequest request);
        Task<ClienteOneResponse>GetOneCliente(int id_cliente);
        Task<string>Update(int id_cliente,ClienteRequest request);
    }
}
