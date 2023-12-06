
-- Seleccionar la base de datos
USE DBPractica4;

-- Crear la tabla "Principal" si no existe
CREATE TABLE Principal (
    codCompra INT PRIMARY KEY,
    descripcion VARCHAR(255),
    precio DECIMAL(10, 2),
    saldo DECIMAL(10, 2),
    estado VARCHAR(50)
);

-- Crear la tabla "Abonos" si no existe
CREATE TABLE Abonos (
	id INT IDENTITY(1,1) PRIMARY KEY,
    codCompra INT,
    abono DECIMAL(10, 2)
);

-- Insertar datos en la tabla "Principal"
INSERT INTO Principal (codCompra, descripcion, precio, saldo, estado)
VALUES
    (1, 'Descripci�n 1', 20.00, 0.00, 'Cancelado'),
    (2, 'Descripci�n 2', 30.00, 25.00, 'Pendiente'),
    (3, 'Descripci�n 3', 40.00, 35.00, 'Pendiente'),
    (4, 'Descripci�n 4', 50.00, 40.00, 'Pendiente'),
    (5, 'Descripci�n 5', 60.00, 0.00, 'Cancelado');

-- Insertar datos en la tabla "Abonos"
INSERT INTO Abonos (codCompra, abono)
VALUES
    (1, 5.00),
	(1, 15.00),
    (2, 5.00),
    (3, 5.00),
    (4, 10.00),
    (5, 25.00),
    (5, 20.00),
    (5, 15.00);
