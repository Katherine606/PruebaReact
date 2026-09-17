
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReservaPrueba.Models.Entities
{
    
    public class Sala
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public int Capacidad { get; set; }

        [Required, MaxLength(30)]
        public string Estado { get; set; } = "Disponible";

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
