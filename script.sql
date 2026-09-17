
Create database  db_reserva;
GO

USE db_reserva;
GO


CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(MAX) NOT NULL,
    Rol VARCHAR(20) NOT NULL DEFAULT 'User'
);


CREATE TABLE Salas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Capacidad INT NOT NULL,
    Estado VARCHAR(30) NOT NULL DEFAULT 'Disponible'
);

CREATE TABLE Reservas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    SalaId INT NOT NULL,
    Fecha DATE NOT NULL,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    Motivo VARCHAR(250) NOT NULL,
    Estado VARCHAR(30) NOT NULL DEFAULT 'Activa',
    
    CONSTRAINT FK_Reservas_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
    CONSTRAINT FK_Reservas_Salas FOREIGN KEY (SalaId) REFERENCES Salas(Id)
);
GO

--sp Resumen de Reservas con Sala y Usuario
CREATE PROCEDURE sp_ObtenerResumenReservas
AS
BEGIN
    SELECT r.Id, r.Fecha, r.HoraInicio, r.HoraFin, r.Motivo, r.Estado,
        u.Id AS UsuarioId,u.Nombre AS UsuarioNombre,u.Correo AS UsuarioCorreo,
        s.Id AS SalaId,s.Nombre AS SalaNombre,s.Capacidad, s.Estado AS SalaEstado
    FROM Reservas r INNER JOIN Usuarios u ON r.UsuarioId = u.Id
                    INNER JOIN Salas s ON r.SalaId = s.Id;
END
GO

INSERT INTO Usuarios (Nombre, Correo, PasswordHash, Rol) VALUES 
('Juan alcachofa', 'juan13@empresa.com', 'AaAAAAEAACcQAAAAEEEEEAdmin123==', 'Admin'),
('Lesly Campos', 'lesly12@gmail.com', 'AaAAAAEAACcQAAAAEEEEEUser123==', 'User');
GO


INSERT INTO Salas (Nombre, Capacidad, Estado) VALUES 
('Sala Directorio', 12, 'Disponible'),
('Sala Innovación', 6, 'Disponible'),
('Sala Ejecutiva 1', 4, 'FueraDeServicio');
GO


select * from Usuarios