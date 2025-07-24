﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Entities
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public bool EstaActivo { get; set; }

        public int? TiendaId { get; set; }
        public Tienda? Tienda { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
