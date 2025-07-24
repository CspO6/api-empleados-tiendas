using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<List<Empleado>> GetAllAsync();
        Task<Empleado?> GetByIdAsync(int id);
        Task AddAsync(Empleado empleado);
        Task UpdateAsync(Empleado empleado);
        Task DeleteAsync(int id);
    }
}
