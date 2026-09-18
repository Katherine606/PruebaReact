using Microsoft.EntityFrameworkCore;
using ReservaPrueba.Models.Entities;

namespace ReservaPrueba.Repositories
{
    public class AuthRepository
    {
        private readonly AppDbContext _context;
        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerPorCorreoAsync(string correo)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }
    }
}
