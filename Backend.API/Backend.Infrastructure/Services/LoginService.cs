using Backend.Application.Interfaces;
using Backend.Domain.DTOs;
using Backend.Infrastructure.Persistence;
using Backend.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

            var usuario = await _context.Usuarios
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u =>
                    u.NombreUsuario.ToLower() == dto.Usuario.ToLower().Trim() &&
                    u.Clave == dto.Clave.Trim() &&
                    u.EstaActivo);

            if (usuario == null)
            {
                Console.WriteLine("No se encontró ningún usuario con esas credenciales.");
                return null;
            }

            Console.WriteLine("Usuario autenticado correctamente:");
            Console.WriteLine($"Nombre de usuario: {usuario.NombreUsuario}");
            Console.WriteLine($"Rol: {usuario.Rol}");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol ?? "Empleado")
            };

            // Si tiene un empleado asociado, agregar su nombre como claim adicional (opcional)
            if (usuario.Empleado != null)
            {
                claims.Add(new Claim("EmpleadoNombre", usuario.Empleado.Nombre));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }

}
