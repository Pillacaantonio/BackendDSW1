using Dapper;
using ProyectoApi.Helpers;
using ProyectoApi.Models.Request;
using ProyectoApi.Models.Response;
using ProyectoApi.Repositories.interfaces;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly IDatabaseExecutor _executor;

        public FacturaRepository(IDatabaseExecutor executor)
        {
            _executor = executor;
        }

        public async Task<string> CrearFactura(FacturaRequest request)
        {
            var sp = "InsertarFacturaCompleta";
            var parameters = new DynamicParameters();

            parameters.Add("ClienteId", request.ClienteId);
            parameters.Add("Fecha", request.Fecha);
            parameters.Add("Estado", request.Estado);
            parameters.Add("TipoDocumento", request.TipoDocumento);

            var detalleTable = new DataTable();
            detalleTable.Columns.Add("ProductoId", typeof(int));
            detalleTable.Columns.Add("Cantidad", typeof(int));
            detalleTable.Columns.Add("PrecioUnitario", typeof(decimal));

            foreach (var detalle in request.Detalles)
            {
                detalleTable.Rows.Add(detalle.ProductoId, detalle.Cantidad, detalle.PrecioUnitario);
            }

            parameters.Add("DetallesFactura", detalleTable.AsTableValuedParameter("TipoDetalleFactura"));

            await _executor.ExecuteCommand(conn =>
                conn.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure));

            return "Factura creada correctamente.";
        }

        public async Task<IEnumerable<FacturaResponse>> GetFacturas()
        {
            var sp = "sp_GetAllFacturas";
            var result = await _executor.ExecuteCommand(conn =>
                conn.QueryAsync<FacturaResponse>(sp, commandType: CommandType.StoredProcedure));
            return result;
        }

        public async Task<FacturaOneResponse> GetOneFactura(int idFactura, DateTime fecha, int clienteId)
        {
            var sp = "sp_GetOneFacturaById";

            var parameters = new DynamicParameters();
            parameters.Add("FacturaId", idFactura, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Fecha", fecha.Date, DbType.Date, ParameterDirection.Input); // Solo la fecha sin hora
            parameters.Add("ClienteId", clienteId, DbType.Int32, ParameterDirection.Input);

            var result = await _executor.ExecuteCommand(async conexion =>
            {
                // Ejecutar el procedimiento con múltiples resultados
                var multi = await conexion.QueryMultipleAsync(sp, parameters, commandType: CommandType.StoredProcedure);

                // Leer el resultado de la factura (cabecera)
                var factura = await multi.ReadSingleOrDefaultAsync<FacturaOneResponse>();

                // Leer los detalles de la factura
                var detalles = (await multi.ReadAsync<FacturaDetalleResponse>()).ToList();

                if (factura != null)
                {
                    factura.Detalles = detalles;
                }

                return factura;
            });

            return result;
        }

        public async Task<string> UpdateEstado(int idFactura, string nuevoEstado, int cantidad)
        {
            var sp = "sp_UpdateEstadoFactura";
            var parameters = new DynamicParameters();
            parameters.Add("@FacturaId", idFactura, DbType.Int32);
            parameters.Add("@NuevoEstado", nuevoEstado, DbType.String);
            parameters.Add("@Cantidad", cantidad, DbType.Int32); // Pasar el valor de la cantidad

            // Ejecutar el procedimiento almacenado y obtener el número de filas afectadas
            var filasAfectadas = await _executor.ExecuteCommand(conn =>
                conn.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure));

            // Devolver el mensaje adecuado según el número de filas afectadas
            if (filasAfectadas > 0)
            {
                return "Estado actualizado correctamente.";
            }
            else
            {
                return "No se actualizó el estado. Puede que no exista la factura o el estado ya esté actualizado.";
            }
        }
    }
}
