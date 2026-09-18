using Microsoft.IdentityModel.Tokens;
using ReservaPrueba.Exceptions;
using ReservaPrueba.Models.DTOs;
using ReservaPrueba.Models.Entities;
using ReservaPrueba.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReservaPrueba.Services
{
    public class AuthService
    {
        private readonly AuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(AuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }



        public async Task<string> LoginAsync(LoginDto dto)
        {
            var usuario = await _authRepository.ObtenerPorCorreoAsync(dto.Correo);

            if(usuario == null)
            {
                 throw new ApiException("El usuario con este correo no existe", 404);
            }

            bool passwordV = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);
            if (!passwordV)
            {
                throw new ApiException("Contraseña incorrecta", 400);
            }

            return GenerarTokenJwt(usuario);
            
        }

        private string GenerarTokenJwt(Usuario usuario)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim(ClaimTypes.Name, usuario.Nombre)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}