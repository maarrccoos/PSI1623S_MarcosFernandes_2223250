CREATE DATABASE gestaonotas
USE gestaonotas

CREATE TABLE Categoria (
    id INT PRIMARY KEY IDENTITY(1,1),
    nome NVARCHAR(100) NOT NULL
);


CREATE TABLE EstadoNota (
    id INT PRIMARY KEY IDENTITY(1,1),
    nome NVARCHAR(50) NOT NULL
);

CREATE TABLE Nota (
    id INT PRIMARY KEY IDENTITY(1,1),
    titulo NVARCHAR(150) NOT NULL,
    texto NVARCHAR(MAX),
    dataCriacao DATETIME DEFAULT GETDATE(),
    categoriaId INT NOT NULL,
    estadoNotaId INT NOT NULL,
    FOREIGN KEY (categoriaId) REFERENCES categoria(id),
    FOREIGN KEY (estadoNotaId) REFERENCES estadoNota(id)
); 