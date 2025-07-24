using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTOs
{
    public class EmpleadoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El cargo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El cargo no puede tener más de 50 caracteres.")]
        public string Cargo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        public DateTime FechaIngreso { get; set; }

        public bool EstaActivo { get; set; }

        public int? TiendaId { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres.")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La clave es obligatoria.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "La clave debe tener entre 4 y 100 caracteres.")]
        public string Clave { get; set; } = string.Empty;
    }
}
