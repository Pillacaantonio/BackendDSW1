using ProyectoApi.Models.Request;

namespace ProyectoApi.Models.Response
{
    public class FacturaOneResponse
    {
        public int FacturaId { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IGV { get; set; }
        public decimal Total { get; set; }
        public string TipoDocumento { get; set; }
        public string Estado { get; set; }

        // Aquí agregamos la propiedad Detalles para almacenar los detalles de la factura
        public List<FacturaDetalleResponse> Detalles { get; set; }
    }
}
