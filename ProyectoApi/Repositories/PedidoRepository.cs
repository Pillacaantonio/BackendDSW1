using Dapper;
using ProyectoApi.Helpers;
using ProyectoApi.Models.Request;
using ProyectoApi.Repositories.interfaces;

namespace ProyectoApi.Repositories
{
    public class PedidosRepository : IPedidosRepository
    {
        private readonly IDatabaseExecutor _executor;

        public PedidosRepository(IDatabaseExecutor executor)
        {
            _executor = executor;
        }

        public async Task<int> CrearPedido(PedidoRequest request)
        {
            return await _executor.ExecuteCommand(async conn => {
                await conn.OpenAsync();
                using var transaction = conn.BeginTransaction();

                var insertPedido = @"INSERT INTO Pedidos (ClienteId, Fecha, Total)
                                     VALUES (@ClienteId, GETDATE(), @Total);
                                     SELECT CAST(SCOPE_IDENTITY() as int);";
                var pedidoId = await conn.ExecuteScalarAsync<int>(insertPedido,
                    new { request.ClienteId, request.Total }, transaction);

                var insertDetalle = @"INSERT INTO PedidoDetalles (PedidoId, ProductoId, Cantidad, PrecioUnitario)
                                      VALUES (@PedidoId, @ProductoId, @Cantidad, @PrecioUnitario);";
                foreach (var item in request.Items)
                {
                    await conn.ExecuteAsync(insertDetalle, new
                    {
                        PedidoId = pedidoId,
                        ProductoId = item.ProductId,
                        Cantidad = item.Quantity,
                        PrecioUnitario = item.Price
                    }, transaction);
                }

                transaction.Commit();
                return pedidoId;
            });
        }
    }
}