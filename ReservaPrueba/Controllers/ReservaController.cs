using Microsoft.AspNetCore.Mvc;

using ReservaPrueba.Models.DTOs;
using ReservaPrueba.Services;

namespace ReservaPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservaController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

 
        [HttpGet("resumen")]
        public async Task<ActionResult> ObtenerResumen()
        {
           
            var resultado = await _reservaService.ObtenerResumenAsync();
            return Ok(resultado);
       
        
        }


        [HttpPost("crear")]
        public async Task<ActionResult> CrearReserva([FromBody] ReservaCrearDto dto)
        {
          
            var id = await _reservaService.CrearReservaAsync(dto);
            return Ok(new { mensaje = "Reserva creada con éxito", id });
         
        }
        [HttpPatch("cancelar/{id}")]
        public async Task<ActionResult> CancelarReserva(int id)
        {
            
             await _reservaService.CancelarReservaAsync(id);
             return Ok(new { mensaje = $"Reserva con ID {id} cancelada con éxito." });
            
           
        }
    }
}