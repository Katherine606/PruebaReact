using System.ComponentModel.DataAnnotations;

namespace ReservaPrueba.Models.DTOs
{
    public class ReservaCrearDto
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El ID de la sala es obligatorio.")]
        public int SalaId { get; set; }

        [Required(ErrorMessage = "La fecha de la reserva es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "El formato de la fecha no es válido.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de finalización es obligatoria.")]
        public TimeSpan HoraFin { get; set; }

        [Required(ErrorMessage = "El motivo de la reserva es obligatorio.")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "El motivo debe tener entre 3 y 250 caracteres.")]
        public string Motivo { get; set; } = string.Empty;
    }
}