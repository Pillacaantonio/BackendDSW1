namespace ProyectoApi.Models.Request
{
    public class PedidoRequest
    {
        public int ClienteId { get; set; }
        public decimal Total { get; set; }
        public List<PedidoDetalleRequest>? Items { get; set; }
    }
}
