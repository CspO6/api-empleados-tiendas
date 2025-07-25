﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Entities
{
    public class Tienda
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public bool EstaActiva { get; set; }

        // Relación uno a muchos
        public ICollection<Empleado>? Empleados { get; set; }
    }
}
