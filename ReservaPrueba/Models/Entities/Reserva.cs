
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReservaPrueba.Models.Entities
{

    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }

        [Required]
        public int SalaId { get; set; }
        [ForeignKey("SalaId")]
        public Sala? Sala { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }

        [Required, MaxLength(250)]
        public string Motivo { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string Estado { get; set; } = "Activa";
    }
}
