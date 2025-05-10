using Dapper;
using Microsoft.Data.SqlClient;
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

            try
            {
                var sp = "USP_INSERT_CLIENTE";
                var parameters = new DynamicParameters();

                parameters.Add("Nombre", request.Nombre, DbType.String, ParameterDirection.Input);
                parameters.Add("Apellido", request.Apellido, DbType.String, ParameterDirection.Input);
                parameters.Add("DocumentoIdentidad", request.DocumentoIdentidad, DbType.String, ParameterDirection.Input);
                parameters.Add("Direccion", request.Direccion, DbType.String, ParameterDirection.Input);
                parameters.Add("Telefono", request.Telefono, DbType.String, ParameterDirection.Input);
                parameters.Add("Email", request.Email, DbType.String, ParameterDirection.Input);

                var resultado = await _executor.ExecuteCommand(conexion => conexion.ExecuteAsync(sp, parameters));

                return $"Se ha registrado {resultado}";
            }
            catch (Exception ex)
            {
                 return $"Ocurrió un error: {ex.Message}";
            }
        }

      

        public async Task<ClienteOneResponse> GetOneCliente(int id_cliente)
        {
            var sp = "USP_GET_ONE_CLIENTE";

            var parameters = new DynamicParameters();
            parameters.Add("id_cliente", id_cliente, DbType.Int32, ParameterDirection.Input);

            var result = await _executor.ExecuteCommand(conexion => conexion.QuerySingleAsync<ClienteOneResponse>(sp, parameters));

            return result;
        }

        public async Task<IEnumerable<ClienteResponse>> ListadoClientes()
        {
            var sp = "SP_SEARCH_ALL_CLIENTE";
            var parameters = new DynamicParameters(); 

            var listado = await _executor.ExecuteCommand(conexion => conexion.QueryAsync<ClienteResponse>(sp, parameters));

            return listado;
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
            try
            {
                var sp = "USP_UPDATE_CLIENTE";
                var parameters = new DynamicParameters();
                parameters.Add("id_cliente", id_cliente, DbType.Int32, direction: ParameterDirection.Input);
                parameters.Add("Nombre", request.Nombre, DbType.String, ParameterDirection.Input);
                parameters.Add("Apellido", request.Apellido, DbType.String, ParameterDirection.Input);
                parameters.Add("DocumentoIdentidad", request.DocumentoIdentidad, DbType.String, ParameterDirection.Input);
                parameters.Add("Direccion", request.Direccion, DbType.String, ParameterDirection.Input);
                parameters.Add("Telefono", request.Telefono, DbType.String, ParameterDirection.Input);
                parameters.Add("Email", request.Email, DbType.String, ParameterDirection.Input);
                parameters.Add("FechaRegistro", request.FechaRegistro, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("Estado", request.Etado, DbType.Boolean, ParameterDirection.Input);


                var resultado = await _executor.ExecuteCommand(conexion => conexion.ExecuteAsync(sp, parameters));

                return $"Se ha actualizado correctamente el cliente con código {id_cliente}";
            }
            catch (SqlException ex)
            {
                 return  ($"Error al actualizar el cliente: {ex.Message}");
            }
        }
        public async Task<string> EliminarCliente(int id_cliente)
        {
            try
            {
                var sp = "USP_ELIMINAR_CLIENTE"; 

                var parameters = new DynamicParameters();
                parameters.Add("id_cliente", id_cliente, DbType.Int32, ParameterDirection.Input);

                var result = await _executor.ExecuteCommand(conexion => conexion.ExecuteAsync(sp, parameters));

                if (result > 0)
                {
                    return "OK";   
                }
                else
                {
                    return "Error al eliminar el cliente";   
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }

    }
}
