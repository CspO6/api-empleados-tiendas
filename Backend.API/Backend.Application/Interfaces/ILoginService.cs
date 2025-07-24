using Backend.Application.DTOs;
using Backend.Domain.DTOs;

namespace Backend.Application.Interfaces
{
    public interface ILoginService
    {
        Task<string?> LoginAsync(LoginRequestDto dto);
    }
}
