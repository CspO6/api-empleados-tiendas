namespace Backend.Application.DTOs
{
    public class EmpleadoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public bool EstaActivo { get; set; }
        public int? TiendaId { get; set; }
    }
}
