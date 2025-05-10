using Dapper;
using ProyectoApi.Helpers;
using ProyectoApi.Models.Queries;
using ProyectoApi.Models.Request;
using ProyectoApi.Models.Response;
using ProyectoApi.Repositories.interfaces;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDatabaseExecutor _executor;

        public ClienteRepository(IDatabaseExecutor executor)
        {
            _executor = executor;
        }
        public async Task<string> CrearCliente(ClienteRequest request)
        {
            var sp = "USP_INSERT_CLIENTE";

            var parameters = new DynamicParameters();

           // parameters.Add("IdVendedor", DbType.Int32, direction: ParameterDirection.Output);

            //Enviamos lo demás
            parameters.Add("Nombre", request.Nombre, DbType.String, ParameterDirection.Input);
            parameters.Add("Apellido", request.Apellido, DbType.String, ParameterDirection.Input);
            parameters.Add("DocumentoIdentidad", request.DocumentoIdentidad, DbType.String, ParameterDirection.Input);
            parameters.Add("Direccion", request.Direccion, DbType.String, ParameterDirection.Input);
            parameters.Add("Telefono", request.Telefono, DbType.String, ParameterDirection.Input);
            parameters.Add("Email", request.Email, DbType.String, ParameterDirection.Input);
 

            var resultado = await _executor.ExecuteCommand(conexion => conexion.ExecuteAsync(sp, parameters));

            //var idGenerado = parameters.Get<int>("IdVendedor");

            return $"Se ha registrado {resultado}  ";
        }

        public async Task<ClienteOneResponse> GetOneCliente(int id_cliente)
        {
            var sp = "USP_GET_ONE_CLIENTE";

            var parameters = new DynamicParameters();
            parameters.Add("id_cliente", id_cliente, DbType.Int32, ParameterDirection.Input);

            var result = await _executor.ExecuteCommand(conexion => conexion.QuerySingleAsync<ClienteOneResponse>(sp, parameters));

            return result;
        }

        public async  Task<IEnumerable<ClienteResponse>> SearchClientes(ClienteQuery query)
        {
            var sp = "SP_SEARCH_CLIENTE";
            var parameters = new DynamicParameters();
            parameters.Add("id_cliente", query.id_cliente, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Apellido", query.Apellido, DbType.String, ParameterDirection.Input);

            var listado = await _executor.ExecuteCommand(conexion => conexion.QueryAsync<ClienteResponse>(sp, parameters));

            return listado;
        }

        public async Task<string> Update(int id_cliente, ClienteRequest request)
        {
            var sp = "USP_UPDATE_CLIENTE";

            var parameters = new DynamicParameters();

            parameters.Add("id_cliente", id_cliente, DbType.Int32, direction: ParameterDirection.Input);

            //Enviamos lo demás
            parameters.Add("Nombre", request.Nombre, DbType.String, ParameterDirection.Input);
            parameters.Add("Apellido", request.Apellido, DbType.String, ParameterDirection.Input);
            parameters.Add("DocumentoIdentidad", request.DocumentoIdentidad, DbType.String, ParameterDirection.Input);
            parameters.Add("Direccion", request.Direccion, DbType.String, ParameterDirection.Input);
            parameters.Add("Telefono", request.Telefono, DbType.String, ParameterDirection.Input);
            parameters.Add("Email", request.Email, DbType.String, ParameterDirection.Input);
            parameters.Add("FechaRegistro", request.FechaRegistro, DbType.DateTime, ParameterDirection.Input);

            var resultado = await _executor.ExecuteCommand(conexion => conexion.ExecuteAsync(sp, parameters));

            return $"Se ha actualizado {resultado} vendedor con código {id_cliente}";
        }
    }
}
