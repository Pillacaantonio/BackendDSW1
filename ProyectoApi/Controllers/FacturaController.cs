using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models.Queries;
using ProyectoApi.Models.Request;
using ProyectoApi.Models.Response;
using ProyectoApi.Repositories.interfaces;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaController(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        // Obtener todas las facturas
        [HttpGet("GetFacturas")]
        public async Task<ActionResult<IEnumerable<FacturaResponse>>> GetFacturas()
        {
            var facturas = await _facturaRepository.GetFacturas();
            return Ok(facturas);
        }

        // Obtener una factura por ID
        [HttpGet("GetOneFactura/{idFactura}")]
        public async Task<ActionResult<FacturaResponse>> GetOneFactura(int idFactura, [FromQuery] DateTime fecha, [FromQuery] int clienteId)
        {
            var factura = await _facturaRepository.GetOneFactura(idFactura, fecha, clienteId);
            if (factura == null)
                return NotFound("Factura no encontrada.");
            return Ok(factura);
        }

        // Actualizar el estado de una factura
        [HttpPut("updateestado/{idFactura}")]
        public async Task<IActionResult> UpdateEstado(int idFactura, [FromBody] FacturaQuery request)
        {
            if (request == null || string.IsNullOrEmpty(request.NuevoEstado) || request.Cantidad <= 0)
            {
                return BadRequest("Datos inválidos o incompletos.");
            }

            try
            {
                // Llamar al servicio para actualizar el estado
                var resultado = await _facturaRepository.UpdateEstado(idFactura, request.NuevoEstado, request.Cantidad);

                if (resultado == "Estado actualizado correctamente.")
                {
                    return Ok(resultado); // Respuesta exitosa
                }
                else
                {
                    return BadRequest(resultado); // Error de actualización
                }
            }
            catch (Exception ex)
            {
                // Log o manejo de errores si es necesario
                return StatusCode(500, $"Ocurrió un error al actualizar el estado: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CrearFactura([FromBody] FacturaRequest request)
        {
            if (request == null || request.Detalles == null || request.Detalles.Count == 0)
            {
                return BadRequest("La solicitud debe incluir una factura válida con al menos un detalle.");
            }

            try
            {
                var resultado = await _facturaRepository.CrearFactura(request);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la factura.", detalle = ex.Message });
            }
        }
    }
}


