using Dapper;
using ReservaPrueba.Models.DTOs;
using System.Data;

namespace ReservaPrueba.Repositories
{

    public class ReservaRepository
    {
        private readonly IDbConnection _dbConnection;

        public ReservaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<ReservaResumenDto>> ObtenerResumenReservasAsync()
        {
            var resultado = await _dbConnection.QueryAsync<ReservaResumenDto>("sp_ObtenerResumenReservas", commandType: CommandType.StoredProcedure );
            return resultado;
        }
    }
}