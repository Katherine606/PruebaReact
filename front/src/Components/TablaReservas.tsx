import type { ReservaResumen } from '../types/reserva';

interface Props {
  reservas: ReservaResumen[];
  onCancelar: (id: number) => void;
}

export const TablaReservas = ({ reservas, onCancelar }: Props) => {
  return (
    <div className="table-responsive">
      <table className="table table-hover align-middle">
        <thead className="table-light">
          <tr>
            <th>#</th>
            <th>Usuario</th>
            <th>Sala</th>
            <th>Fecha</th>
            <th>Horario</th>
            <th>Motivo</th>
            <th>Estado</th>
            <th className="text-end">Acciones</th>
          </tr>
        </thead>
        <tbody>
          {reservas.length === 0 ? (
            <tr>
              <td colSpan={8} className="text-center py-4 text-muted">
                No hay reservas registradas.
              </td>
            </tr>
          ) : (
            reservas.map((reserva) => (
              <tr key={reserva.id}>
                <td className="fw-bold">{reserva.id}</td>
                <td>{reserva.usuarioNombre}</td>
                <td>{reserva.salaNombre}</td>
                <td>{new Date(reserva.fecha).toLocaleDateString()}</td>
                <td>{reserva.horaInicio} - {reserva.horaFin}</td>
                <td className="text-truncate" style={{ maxWidth: '200px' }}>
                  {reserva.motivo}
                </td>
                <td>
                  <span className={`badge ${reserva.estado === 'Activa' ? 'bg-success' : 'bg-secondary'}`}>
                    {reserva.estado}
                  </span>
                </td>
                <td className="text-end">
                  {reserva.estado === 'Activa' && ( <button className="btn btn-sm btn-outline-danger px-3 py-1"onClick={() => onCancelar(reserva.id)}>Cancelar</button>
                  )}
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
};