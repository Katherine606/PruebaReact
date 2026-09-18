using Microsoft.AspNetCore.Mvc;
using ReservaPrueba.Models.DTOs;
using ReservaPrueba.Services;

namespace ReservaPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
    
        private readonly AuthService _authService;


        public AuthController (AuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);

            return Ok(new
            {
                mensaje = "Login exitoso",
                token = token
            });
        }

    }
}
