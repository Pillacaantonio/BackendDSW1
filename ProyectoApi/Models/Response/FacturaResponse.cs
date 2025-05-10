using ProyectoApi.Models.Request;

namespace ProyectoApi.Models.Response
{
    public class FacturaResponse
    { 
        
            public int FacturaId { get; set; }
            public int ClienteId { get; set; }
            public string TipoDocumento { get; set; }
            public DateTime Fecha { get; set; }
            public string Estado { get; set; }
            public decimal Subtotal { get; set; }
            public decimal IGV { get; set; }
            public decimal Total { get; set; }
            public List<FacturaDetalleResponse> Detalles { get; set; }
        }

    }
