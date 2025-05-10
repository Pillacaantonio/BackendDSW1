namespace ProyectoApi.Models.Request
{
    public class PedidoDetalleRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
