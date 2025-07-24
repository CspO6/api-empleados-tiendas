using Backend.Application.DTOs;

namespace Backend.Application.Interfaces
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDto>> GetAllAsync();
        Task<EmpleadoDto?> GetByIdAsync(int id);
        Task<EmpleadoDto> CreateAsync(EmpleadoDto dto);
        Task<bool> UpdateAsync(EmpleadoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
