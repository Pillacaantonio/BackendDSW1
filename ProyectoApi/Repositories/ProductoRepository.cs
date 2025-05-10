using Dapper;
using ProyectoApi.Helpers;
using ProyectoApi.Models.Request;
using ProyectoApi.Models.Response;
using ProyectoApi.Repositories.interfaces;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IDatabaseExecutor _executor;

        public ProductoRepository(IDatabaseExecutor executor)
        {
            _executor = executor;
        }
        public async Task<string> CrearProducto(ProductoRequest request)
        {
            var sp = "USP_INSERT_PRODUCTO";

            var parameters = new DynamicParameters();

            // parameters.Add("IdVendedor", DbType.Int32, direction: ParameterDirection.Output);

            //Enviamos lo demás
            parameters.Add("Nombre", request.Nombre, DbType.String, ParameterDirection.Input);
            parameters.Add("Descripcion", request.Descripcion, DbType.String, ParameterDirection.Input);
            parameters.Add("Precio", request.Precio, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("Stock", request.Stock, DbType.Int32, ParameterDirection.Input);
  

            var resultado = await _executor.ExecuteCommand(conexion => conexion.ExecuteAsync(sp, parameters));

            //var idGenerado = parameters.Get<int>("IdVendedor");

            return $"Se ha registrado {resultado}  ";
        }

        public async Task<ProductoOneResponse> GetOneProducto(int id_producto)
        {
            var sp = "USP_GET_ONE_PRODUCTO";

            var parameters = new DynamicParameters();
            parameters.Add("id_producto", id_producto, DbType.Int32, ParameterDirection.Input);

            var result = await _executor.ExecuteCommand(conexion => conexion.QuerySingleAsync<ProductoOneResponse>(sp, parameters));

            return result;
        }

        public async Task<IEnumerable<ProductoResponse>> SearchProducto()
        {
            var sp = "SP_SEARCH_PRODUCTO";
            var parameters = new DynamicParameters();
  
            var listado = await _executor.ExecuteCommand(conexion => conexion.QueryAsync<ProductoResponse>(sp, parameters));

            return listado;
        }

        public async Task<string> Update(int id_producto, ProductoRequest request)
        {
            var sp = "USP_UPDATE_PRODUCTO";   

            var parameters = new DynamicParameters();

             parameters.Add("@IdProducto", id_producto, DbType.Int32, ParameterDirection.Input); 

            // Enviar el resto de parámetros
            parameters.Add("@Nombre", request.Nombre, DbType.String, ParameterDirection.Input);
            parameters.Add("@Descripcion", request.Descripcion, DbType.String, ParameterDirection.Input);
            parameters.Add("@Precio", request.Precio, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@Stock", request.Stock, DbType.Int32, ParameterDirection.Input);
 
            // Ejecutar el procedimiento almacenado
            var resultado = await _executor.ExecuteCommand(conexion =>
                conexion.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure)
            );

             return $"Se ha actualizado el producto con código {id_producto}. Filas afectadas: {resultado}";
        }

    }
}
