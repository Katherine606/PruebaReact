import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Login } from './Components/Login';
import { TablaReservas } from './Components/TablaReservas';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/login" element={<Login />} />
        <Route path="/prestamos" element={<TablaReservas />} />
      </Routes>
    </Router>
  );
}

export default App;