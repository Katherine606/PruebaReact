import { useEffect, useState } from 'react';
import { obtenerResumenReservas, cancelarReserva } from './services/reservaService';
import { TablaReservas } from './Components/TablaReservas';
import type { ReservaResumen } from './types/reserva';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  const [reservas, setReservas] = useState<ReservaResumen[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const cargarReservas = async () => {
    try {
      setLoading(true);
      const data = await obtenerResumenReservas();

      setReservas(Array.isArray(data) ? data : []);
      setError(null);
    } catch (err) {
      setError('Error al cargar las reservas desde la API.');
      setReservas([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    cargarReservas();
  }, []);

  const handleCancelar = async (id: number) => {
    if (!window.confirm(`¿Estás seguro de cancelar la reserva #${id}?`)) return;
    try {
      await cancelarReserva(id);
      cargarReservas();
    } catch (err) {
      alert('No se pudo cancelar la reserva.');
    }
  };

  return (
    <div className="container mt-5">
      <h2 className="fw-bold text-primary mb-4">Listado de Reservas</h2>

      {error && <div className="alert alert-danger">{error}</div>}

      {loading ? (
        <div className="text-center py-5">
          <div className="spinner-border text-primary" role="status"></div>
        </div>
      ) : (
        <TablaReservas reservas={reservas} onCancelar={handleCancelar} />
      )}
    </div>
  );
}

export default App;