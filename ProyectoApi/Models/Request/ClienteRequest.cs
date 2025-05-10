namespace ProyectoApi.Models.Request
{
    public class ClienteRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public string Email { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Etado { get; set; }
    }
}
