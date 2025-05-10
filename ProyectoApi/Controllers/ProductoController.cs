using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;
        public ProductoController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ProductoResponse>>> SearchProducto()
        {
            var response = await _productoRepository.SearchProducto();
            return Ok(response);
        }
        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<string>> CrearProducto([FromBody] ProductoRequest request)
        {
            var response = await _productoRepository.CrearProducto(request);
            return Ok(response);
        }

        [HttpGet("[action]/{id_producto}")]
        public async Task<ActionResult<ClienteOneResponse>> GetOneProducto(int id_producto)
        {
            var response = await _productoRepository.GetOneProducto(id_producto);
            return Ok(response);
        }

        [HttpPut("[action]/{id_producto}")]
        public async Task<ActionResult<string>> Update(int id_producto, [FromBody] ProductoRequest request)
        {
            var response = await _productoRepository.Update(id_producto, request);
            return Ok(response);
        }
    }
}
