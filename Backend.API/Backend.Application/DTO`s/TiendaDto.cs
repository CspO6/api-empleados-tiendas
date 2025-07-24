using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTOs
{
    public class TiendaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la tienda es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la tienda no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección de la tienda es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
        public string Direccion { get; set; } = string.Empty;

        public bool EstaActiva { get; set; }
    }
}
