using ProyectoApi.Models.Response;

namespace ProyectoApi.Models.Request
{
    public class FacturaRequest
    {
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoDocumento { get; set; }
        public string Estado { get; set; }
        public List<FacturaDetalleRequest> Detalles { get; set; }
    }
}
