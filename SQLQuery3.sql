CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY,
    NombreUsuario VARCHAR(100),
    Apellido VARCHAR(100),
    Mail VARCHAR(100),
    NumTelefono INT,
    Username VARCHAR(50),
    Contrasena VARCHAR(50)
);

CREATE TABLE Producto (
    IdProducto INT PRIMARY KEY,
    NombreProducto VARCHAR(100),
    PrecioProducto DECIMAL(10, 2),
    Categoria VARCHAR(50),
    Descripcion VARCHAR(255),
    Imagen VARCHAR(255)
);

CREATE TABLE Carrito (
    IdCarrito INT PRIMARY KEY,
    IdUsuario INT,
    IdProducto INT,
    PrecioProducto DECIMAL(10, 2),  
    CantidadProducto int ,
    TotalVenta DECIMAL(10, 2),
    Fecha DATE,
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);
