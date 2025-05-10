using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProyectoApi.Models.Queries;
using ProyectoApi.Models.Request;
using ProyectoApi.Models.Response;
using ProyectoApi.Repositories;
using ProyectoApi.Repositories.interfaces;
using System.Net;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ClienteResponse>>> SearchClientes([FromQuery] ClienteQuery query)
        {
            var response = await _clienteRepository.SearchClientes(query);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<string>> CrearCliente([FromBody] ClienteRequest request)
        {
            try
            {
                var response = await _clienteRepository.CrearCliente(request);
                return Ok(response);  
            }
            catch (Exception ex)
            { 
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error al crear cliente: {ex.Message}");  
            }
        }

        [HttpGet("[action]/{id_cliente}")]
        public async Task<ActionResult<ClienteOneResponse>> GetOneCliente(int id_cliente)
        {
            var response = await _clienteRepository.GetOneCliente(id_cliente);
            return Ok(response);
        }

        [HttpPut("[action]/{id_cliente}")]
        public async Task<ActionResult<string>> Update(int id_cliente, [FromBody] ClienteRequest request)
        {
            try
            {
                if (request.FechaRegistro < new DateTime(1753, 1, 1) || request.FechaRegistro > new DateTime(9999, 12, 31))
                {
                    return BadRequest("Fecha fuera del rango permitido.");
                }

                var response = await _clienteRepository.Update(id_cliente, request);

                if (response.Contains("Error"))
                {
                    return BadRequest(response);  
                }

                return Ok(response);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al actualizar el cliente: {ex.Message}");
            }
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ClienteResponse>>> ListadoClientes()
        {
            var response = await _clienteRepository.ListadoClientes();
            return Ok(response);
        }
        [HttpDelete("EliminarCliente/{id_cliente}")]
        public async Task<IActionResult> EliminarCliente(int id_cliente)
        {
            var result = await _clienteRepository.EliminarCliente(id_cliente);
            if (result == "OK")
            {
                return Ok("Cliente eliminado con éxito");
            }
            return BadRequest("Hubo un problema al eliminar el cliente");
        }
    }
}
