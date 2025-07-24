using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Domain.DTOs;
using Backend.Domain.Entities;
using Backend.Infrastructure.Persistence;
using Backend.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public LoginService(AppDbContext context, IOptions<JwtSettings> jwtOptions)
        {
            _context = context;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<string?> LoginAsync(LoginRequestDto dto)
        {
            Console.WriteLine($"Usuario recibido: {dto.Usuario}");
            Console.WriteLine($"Clave recibida: {dto.Clave}");
            var empleado = await _context.Empleados

                .FirstOrDefaultAsync(e =>
                    e.Usuario.ToLower() == dto.Usuario.ToLower().Trim() &&
                    e.Clave == dto.Clave.Trim() &&
                    e.EstaActivo);
            if (empleado != null)
            {
                Console.WriteLine("Empleado encontrado:");
                Console.WriteLine($"Usuario: {empleado.Usuario}");
                Console.WriteLine($"Clave: {empleado.Clave}");
                Console.WriteLine($"Activo: {empleado.EstaActivo}");
            }
            else
            {
                Console.WriteLine("No se encontró ningún empleado con esas credenciales.");
            }
            if (empleado == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, empleado.Id.ToString()),
            new Claim(ClaimTypes.Name, empleado.Nombre),
            new Claim(ClaimTypes.Role, empleado.Cargo ?? "Empleado")
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
