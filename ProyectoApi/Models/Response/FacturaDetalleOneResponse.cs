using System.Collections.Specialized;

namespace ProyectoApi.Models.Response
{
    public class FacturaDetalleOneResponse
    {
        public int DetalleId { get; set; }
        public int FacturaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
