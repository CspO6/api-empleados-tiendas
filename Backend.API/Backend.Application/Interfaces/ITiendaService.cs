using Backend.Application.DTOs;

namespace Backend.Application.Interfaces
{
    public interface ITiendaService
    {
        Task<List<TiendaDto>> GetAllAsync();
        Task<TiendaDto?> GetByIdAsync(int id);
        Task<TiendaDto> CreateAsync(TiendaDto dto);
        Task<bool> UpdateAsync(TiendaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}