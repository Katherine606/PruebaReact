import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { cancelarReserva } from '../services/reservaService';

interface ReservaResumen {
  id: number;
  usuarioNombre: string;  
  salaNombre: string;
  fecha: string;
  horaInicio: string;
  horaFin: string;
  motivo: string;
  estado: string;
}

export function TablaReservas() {
  const [reservas, setReservas] = useState<ReservaResumen[]>([]);
  const navigate = useNavigate();

  // Validación del token y carga inicial de datos
  useEffect(() => {
    const token = localStorage.getItem('token');
    if (!token) {
      navigate('/login');
      return;
    }

    cargarReservas(token);
  }, [navigate]);

  const cargarReservas = async (token: string) => {
    try {
      const response = await axios.get('/api/Reserva/resumen', {
        headers: { Authorization: `Bearer ${token}` }
      });
      setReservas(response.data);
    } catch (error) {
      console.error('Error al cargar el resumen de la reserva');
    }
  };

  const handleCancelar = async (id: number) => {
    if (!window.confirm(`¿Estás seguro de cancelar la reserva #${id}?`)) return;
    try {
      await cancelarReserva(id);
      const token = localStorage.getItem('token');
      if (token) cargarReservas(token);
    } catch (err) {
      alert('No se pudo cancelar la reserva.');
    }
  };

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Gestión de Reservas</h2>
        <button 
          className="btn btn-outline-danger btn-sm" 
          onClick={() => { localStorage.removeItem('token'); navigate('/login'); }}
        >
          Cerrar Sesión
        </button>
      </div>

      <div className="card shadow-sm p-4">
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
                      {reserva.estado === 'Activa' && (
                        <button 
                          className="btn btn-sm btn-outline-danger px-3 py-1"
                          onClick={() => handleCancelar(reserva.id)}
                        >
                          Cancelar
                        </button>
                      )}
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default TablaReservas;