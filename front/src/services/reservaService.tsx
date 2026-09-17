import axios from "axios";
import type { ReservaResumen } from '../types/reserva';

const API_URL = 'https://localhost:7153/api/Reserva';

export const obtenerResumenReservas = async (): Promise<ReservaResumen[]> => {
  const response = await axios.get(`${API_URL}/resumen`);
  console.log("datos pq no vieneen:", response.data);
  return response.data;
};

export const cancelarReserva = async (id: number) => {
  const response = await axios.patch(`${API_URL}/cancelar/${id}`);
  return response.data;
};
