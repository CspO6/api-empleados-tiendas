using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class TiendaRepository : ITiendaRepository
    {
        private readonly AppDbContext _context;

        public TiendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tienda>> GetAllAsync()
        {
            return await _context.Tiendas.ToListAsync();
        }

        public async Task<Tienda?> GetByIdAsync(int id)
        {
            return await _context.Tiendas.FindAsync(id);
        }

        public async Task AddAsync(Tienda tienda)
        {
            _context.Tiendas.Add(tienda);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tienda tienda)
        {
            _context.Tiendas.Update(tienda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tienda = await _context.Tiendas.FindAsync(id);
            if (tienda != null)
            {
                _context.Tiendas.Remove(tienda);
                await _context.SaveChangesAsync();
            }
        }
    }
}
