using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Services
{
    public class TiendaService : ITiendaService
    {
        private readonly AppDbContext _context;

        public TiendaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TiendaDto>> GetAllAsync()
        {
            return await _context.Tiendas
                .Select(t => new TiendaDto
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    Direccion = t.Direccion,
                    EstaActiva = t.EstaActiva
                })
                .ToListAsync();
        }

        public async Task<TiendaDto?> GetByIdAsync(int id)
        {
            var tienda = await _context.Tiendas.FindAsync(id);
            if (tienda == null) return null;

            return new TiendaDto
            {
                Id = tienda.Id,
                Nombre = tienda.Nombre,
                Direccion = tienda.Direccion,
                EstaActiva = tienda.EstaActiva
            };
        }

        public async Task<TiendaDto> CreateAsync(TiendaDto dto)
        {
            var tienda = new Tienda
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                EstaActiva = dto.EstaActiva
            };

            _context.Tiendas.Add(tienda);
            await _context.SaveChangesAsync();

            dto.Id = tienda.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(TiendaDto dto)
        {
            var tienda = await _context.Tiendas.FindAsync(dto.Id);
            if (tienda == null) return false;

            tienda.Nombre = dto.Nombre;
            tienda.Direccion = dto.Direccion;
            tienda.EstaActiva = dto.EstaActiva;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tienda = await _context.Tiendas.FindAsync(id);
            if (tienda == null) return false;

            _context.Tiendas.Remove(tienda);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
