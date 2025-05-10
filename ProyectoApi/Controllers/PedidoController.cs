using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Repositories.interfaces;
using ProyectoApi.Models.Request;
using ProyectoApi.Models.Response;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidosRepository _pedidosRepository;
        public PedidosController(IPedidosRepository pedidosRepository)
        {
            _pedidosRepository = pedidosRepository;
        }

        [HttpPost("Checkout")]
        public async Task<ActionResult<PedidoResponse>> Checkout([FromBody] PedidoRequest request)
        {
            try
            {
                var pedidoId = await _pedidosRepository.CrearPedido(request);
                return CreatedAtAction(nameof(Checkout), new PedidoResponse{ PedidoId = pedidoId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar el pedido: {ex.Message}");
            }
        }
    }
}