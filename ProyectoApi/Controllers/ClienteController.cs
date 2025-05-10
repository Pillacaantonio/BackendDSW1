using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models.Queries;
using ProyectoApi.Models.Request;
using ProyectoApi.Models.Response;
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
        public async Task<ActionResult<string>> CrearCliente([FromBody] ClienteRequest request)
        {
            var response = await _clienteRepository.CrearCliente(request);
            return Ok(response);
        }

        [HttpGet("[action]/{id_cliente}")]
        public async Task<ActionResult<ClienteOneResponse>> GetOneCliente(int id_cliente)
        {
            var response = await _clienteRepository.GetOneCliente(id_cliente);
            return Ok(response);
        }

        [HttpPut("[action]/{id_cliente}")]
        public async Task<ActionResult<string>> Update(int id_cliente, [FromBody]ClienteRequest  request)
        {
            var response = await _clienteRepository.Update(id_cliente, request);
            return Ok(response);
        }
    }
}
