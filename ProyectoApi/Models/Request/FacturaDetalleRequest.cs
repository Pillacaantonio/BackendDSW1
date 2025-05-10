namespace ProyectoApi.Models.Request
{
    public class FacturaDetalleRequest
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
