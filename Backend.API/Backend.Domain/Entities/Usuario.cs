using Backend.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Clave { get; set; } = string.Empty; 
    public string Rol { get; set; } = "Empleado";
    public bool EstaActivo { get; set; }

    // Relación con Empleado
    public int? EmpleadoId { get; set; }
    public Empleado? Empleado { get; set; }
}
