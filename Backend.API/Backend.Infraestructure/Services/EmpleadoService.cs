using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly AppDbContext _context;

        public EmpleadoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoDto>> GetAllAsync()
        {
            return await _context.Empleados
                .Select(e => new EmpleadoDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Correo = e.Correo,
                    Cargo = e.Cargo,
                    FechaIngreso = e.FechaIngreso,
                    EstaActivo = e.EstaActivo,
                    TiendaId = e.TiendaId
                })
                .ToListAsync();
        }

        public async Task<EmpleadoDto?> GetByIdAsync(int id)
        {
            var e = await _context.Empleados.FindAsync(id);
            if (e == null) return null;

            return new EmpleadoDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Apellido = e.Apellido,
                Correo = e.Correo,
                Cargo = e.Cargo,
                FechaIngreso = e.FechaIngreso,
                EstaActivo = e.EstaActivo,
                TiendaId = e.TiendaId
            };
        }

        public async Task<EmpleadoDto> CreateAsync(EmpleadoDto dto)
        {
            var empleado = new Empleado
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Cargo = dto.Cargo,
                FechaIngreso = dto.FechaIngreso,
                EstaActivo = dto.EstaActivo,
                TiendaId = dto.TiendaId
            };

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            dto.Id = empleado.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(EmpleadoDto dto)
        {
            var empleado = await _context.Empleados.FindAsync(dto.Id);
            if (empleado == null) return false;

            empleado.Nombre = dto.Nombre;
            empleado.Apellido = dto.Apellido;
            empleado.Correo = dto.Correo;
            empleado.Cargo = dto.Cargo;
            empleado.FechaIngreso = dto.FechaIngreso;
            empleado.EstaActivo = dto.EstaActivo;
            empleado.TiendaId = dto.TiendaId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return false;

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
