namespace ReservaPrueba.Models.DTOs
{

    public class ReservaResumenDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        //ysuario
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;
        public string UsuarioCorreo { get; set; } = string.Empty;
        //sala
        public int SalaId { get; set; }
        public string SalaNombre { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public string SalaEstado { get; set; } = string.Empty;
    }
}
