using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface ITiendaRepository
    {
        Task<List<Tienda>> GetAllAsync();
        Task<Tienda?> GetByIdAsync(int id);
        Task AddAsync(Tienda tienda);
        Task UpdateAsync(Tienda tienda);
        Task DeleteAsync(int id);
    }
}
