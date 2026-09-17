using Microsoft.EntityFrameworkCore;
using ReservaPrueba.Exceptions;
using ReservaPrueba.Models.DTOs;
using ReservaPrueba.Models.Entities;
using ReservaPrueba.Repositories;

namespace ReservaPrueba.Services
{
    public class ReservaService
    {
        private readonly AppDbContext _context;
        private readonly ReservaRepository _reservaRepository;

        public ReservaService(AppDbContext context, ReservaRepository reservaRepository)
        {
            _context = context;
            _reservaRepository = reservaRepository;
        }

        public async Task<IEnumerable<ReservaResumenDto>> ObtenerResumenAsync()
        {
            return await _reservaRepository.ObtenerResumenReservasAsync();
        }

        public async Task<int> CrearReservaAsync(ReservaCrearDto dto)
        {
       
            if (dto.Fecha.Date < DateTime.Today)
            {
                throw new ApiException("La fecha de la reserva no puede ser anterior a la fecha actual.", 400);
            }

            if (dto.HoraFin <= dto.HoraInicio)
            {
                throw new ApiException("La hora de finalización deberá ser posterior a la hora de inicio.", 400);
            }

            var sala = await _context.Salas.FindAsync(dto.SalaId);
          
            if (sala == null) throw new ApiException("La sala no existe.", 404);

            if (sala.Estado != "Disponible")
            {
                throw new ApiException("No se puede reservar una sala que se encuentre fuera de servicio.", 400);
            }

            bool hayCruze = await _context.Reservas.AnyAsync(r =>
                r.SalaId == dto.SalaId &&
                r.Estado == "Activa" &&
                r.Fecha.Date == dto.Fecha.Date &&
                r.HoraInicio < dto.HoraFin &&
                r.HoraFin > dto.HoraInicio
            );

            if (hayCruze)
            {
                throw new ApiException("La sala ya cuenta con una reserva activa en ese horario.", 400);
            }

            var nuevaReserva = new Reserva
            {
                UsuarioId = dto.UsuarioId,
                SalaId = dto.SalaId,
                Fecha = dto.Fecha,
                HoraInicio = dto.HoraInicio,
                HoraFin = dto.HoraFin,
                Motivo = dto.Motivo,
                Estado = "Activa"
            };

            _context.Reservas.Add(nuevaReserva);
            await _context.SaveChangesAsync();

            return nuevaReserva.Id;
        }

        public async Task CancelarReservaAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) throw new ApiException("Reserva no encontrada.", 404);

            reserva.Estado = "Cancelada";
            await _context.SaveChangesAsync();
        }
    }
}