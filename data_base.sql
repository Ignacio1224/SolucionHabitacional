/*
* @Author: Ignacio Cabrera - Matias Schmid
* 03/2019
*/


/* DDL */
-- Create a new database called 'SolucionHabitacional'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
FROM sys.databases
WHERE name = N'SolucionHabitacional'
)
CREATE DATABASE SolucionHabitacional
GO

USE SolucionHabitacional;
GO

-- Create a new table called 'PASANTE' in schema 'SolucionHabitacional'
-- Drop the table if it already exists
IF OBJECT_ID('SolucionHabitacional.PASANTE', 'U') IS NOT NULL
DROP TABLE SolucionHabitacional.PASANTE
GO
-- Create the table in the specified schema
CREATE TABLE SolucionHabitacional.PASANTE
(
    userName [NVARCHAR] (254) NOT NULL,
    -- Email
    userPassword [NVARCHAR] (254) NOT NULL,

    CONSTRAINT pk_PASANTE PRIMARY KEY (userName)
);
GO

-- Create a new table called 'PARAMETRO' in schema 'SolucionHabitacional'
-- Drop the table if it already exists
IF OBJECT_ID('SolucionHabitacional.PARAMETRO', 'U') IS NOT NULL
DROP TABLE SolucionHabitacional.PARAMETRO
GO
-- Create the table in the specified schema
CREATE TABLE SolucionHabitacional.PARAMETRO
(
    nombre [NVARCHAR] (254) NOT NULL,
    valor [NVARCHAR] (254) NOT NULL,

    CONSTRAINT pk_PARAMETRO PRIMARY KEY (nombre)
);
GO

-- Create a new table called 'BARRIO' in schema 'SolucionHabitacional'
-- Drop the table if it already exists
IF OBJECT_ID('SolucionHabitacional.BARRIO', 'U') IS NOT NULL
DROP TABLE SolucionHabitacional.BARRIO
GO
-- Create the table in the specified schema
CREATE TABLE SolucionHabitacional.BARRIO
(
    nombre [NVARCHAR] (254),
    descripcion [NVARCHAR] (254) NOT NULL,

    CONSTRAINT pk_BARRIO PRIMARY KEY (nombre),
);
GO

-- Create a new table called 'VIVIENDA' in schema 'SolucionHabitacional'
-- Drop the table if it already exists
IF OBJECT_ID('SolucionHabitacional.VIVIENDA', 'U') IS NOT NULL
DROP TABLE SolucionHabitacional.VIVIENDA
GO
-- Create the table in the specified schema
CREATE TABLE SolucionHabitacional.VIVIENDA
(
    id INT IDENTITY (1, 1) NOT NULL,
    calle [NVARCHAR] (254) NOT NULL,
    nro_puerta INT NOT NULL,
    descripcion [NVARCHAR] (254),
    nro_banios INT NOT NULL,
    nro_dormitorios INT NOT NULL,
    superficie DECIMAL (18, 4) NOT NULL,
    anio_construccion INT,
    precio_base DECIMAL (18, 4) NOT NULL,
    habilitada CHAR (1) NOT NULL,
    vendida CHAR (1) NOT NULL,
    barrio [NVARCHAR] (254) NOT NULL,

    CONSTRAINT pk_VIVIENDA PRIMARY KEY (id),
    CONSTRAINT fk_barrio_VIVIENDA FOREIGN KEY (barrio) REFERENCES BARRIO (nombre),
    CONSTRAINT un_calle_nroPuerta_PASANTE UNIQUE (calle, nro_puerta)
);
GO

-- Create a new table called 'VNUEVA' in schema 'SolucionHabitacional'
-- Drop the table if it already exists
IF OBJECT_ID('SolucionHabitacional.VNUEVA', 'U') IS NOT NULL
DROP TABLE SolucionHabitacional.VNUEVA
GO
-- Create the table in the specified schema
CREATE TABLE SolucionHabitacional.VNUEVA
(
    vivienda INT NOT NULL,
    precio_final DECIMAL (18, 4) NOT NULL

        CONSTRAINT pk_VNUEVA PRIMARY KEY (vivienda),
    CONSTRAINT fk_vivienda_VIVIENDA FOREIGN KEY (vivienda) REFERENCES VIVIENDA (id)
);
GO

-- Create a new table called 'VUSADA' in schema 'SolucionHabitacional'
-- Drop the table if it already exists
IF OBJECT_ID('SolucionHabitacional.VUSADA', 'U') IS NOT NULL
DROP TABLE SolucionHabitacional.VUSADA
GO
-- Create the table in the specified schema
CREATE TABLE SolucionHabitacional.VUSADA
(
    vivienda INT NOT NULL,
    precio_final DECIMAL (18, 4) NOT NULL,
    contribucion DECIMAL (18, 4),

    CONSTRAINT pk_VNUEVA PRIMARY KEY (vivienda),
    CONSTRAINT fk_vivienda_VIVIENDA FOREIGN KEY (vivienda) REFERENCES VIVIENDA (id)
);
GO


/* STORED PROCEDURES */

-- Create a new stored procedure called 'Insert_Pasante' in schema 'SolucionHabitacional'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'SolucionHabitacional'
    AND SPECIFIC_NAME = N'Insert_Pasante'
)
DROP PROCEDURE SolucionHabitacional.Insert_Pasante
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE SolucionHabitacional.Insert_Pasante
    @userName [NVARCHAR] (254) = NULL,
    @userPassword [NVARCHAR] (254) = NULL
AS
SET NOCOUNT ON;

-- Insert rows into table 'PASANTE'
INSERT INTO PASANTE
    ( -- columns to insert data into
    [userName], [userPassword]
    )
VALUES
    ( -- first row: values for the columns in the list above
        @userName, @userPassword
    )
    GO

SELECT CAST(SCOPE_IDENTITY() AS NVARCHAR);

GO

-- Create a new stored procedure called 'Insert_Parametro' in schema 'SolucionHabitacional'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'SolucionHabitacional'
    AND SPECIFIC_NAME = N'Insert_Parametro'
)
DROP PROCEDURE SolucionHabitacional.Insert_Parametro
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE SolucionHabitacional.Insert_Parametro
    @nombre [NVARCHAR] (254) = NULL,
    @valor [NVARCHAR] (254) = NULL
AS
SET NOCOUNT ON;

-- Insert rows into table 'PARAMETRO'
INSERT INTO PARAMETRO
    ( -- columns to insert data into
    [nombre], [valor]
    )
VALUES
    ( -- first row: values for the columns in the list above
        @nombre, @valor
    )
    GO

SELECT CAST(SCOPE_IDENTITY() AS NVARCHAR);

GO

-- Create a new stored procedure called 'Insert_Barrio' in schema 'SolucionHabitacional'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'SolucionHabitacional'
    AND SPECIFIC_NAME = N'Insert_Barrio'
)
DROP PROCEDURE SolucionHabitacional.Insert_Barrio
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE SolucionHabitacional.Insert_Barrio
    @nombre [NVARCHAR] (254) = NULL,
    @descripcion [NVARCHAR] (254) = NULL
AS
SET NOCOUNT ON;

-- Insert rows into table 'BARRIO'
INSERT INTO BARRIO
    ( -- columns to insert data into
    [nombre], [descripcion]
    )
VALUES
    ( -- first row: values for the columns in the list above
        @nombre, @descripcion
    )
    GO

SELECT CAST(SCOPE_IDENTITY() AS NVARCHAR);

GO

-- Create a new stored procedure called 'Insert_Vivienda' in schema 'SolucionHabitacional'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'SolucionHabitacional'
    AND SPECIFIC_NAME = N'Insert_Vivienda'
)
DROP PROCEDURE SolucionHabitacional.Insert_Vivienda
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE SolucionHabitacional.Insert_Vivienda
    @calle [NVARCHAR] (254) = NULL,
    @nro_puerta INT = NULL,
    @descripcion [NVARCHAR] (254) = NULL,
    @nro_banios INT = NULL,
    @nro_dormitorios INT = NULL,
    @superficie DECIMAL (18, 4) = NULL,
    @anio_construccion INT = NULL,
    @precio_base DECIMAL (18, 4) = NULL,
    @habilitada CHAR (1) = 'F',
    @vendida CHAR (1) = 'F',
    @barrio [NVARCHAR] (254) = NULL
AS
SET NOCOUNT ON;

-- Insert rows into table 'VIVIENDA'
INSERT INTO VIVIENDA
    ( -- columns to insert data into
    [calle], [nro_puerta], [descripcion], [nro_banios], [nro_dormitorios], [superficie], [anio_construccion], [precio_base], [habilitada], [vendida], [barrio]
    )
VALUES
    ( -- first row: values for the columns in the list above
        @calle, @nro_puerta, @descripcion, @nro_banios, @nro_dormitorios, @superficie, @anio_construccion, @precio_base, @habilitada, @vendida, @barrio
    )
    GO

SELECT CAST(SCOPE_IDENTITY() AS NVARCHAR);

GO

-- Create a new stored procedure called 'Insert_VNUEVA' in schema 'SolucionHabitacional'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'SolucionHabitacional'
    AND SPECIFIC_NAME = N'Insert_VNUEVA'
)
DROP PROCEDURE SolucionHabitacional.Insert_VNUEVA
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE SolucionHabitacional.Insert_VNUEVA
    @vivienda [INT],
    @precio_final [DECIMAL] (18, 2) = NULL
AS
SET NOCOUNT ON;

-- Insert rows into table 'VNUEVA'
INSERT INTO VNUEVA
    ( -- columns to insert data into
    [vivienda], [precio_final]
    )
VALUES
    ( -- first row: values for the columns in the list above
        @vivienda, @precio_final
    )
    GO

SELECT CAST(SCOPE_IDENTITY() AS NVARCHAR);

GO


-- Create a new stored procedure called 'Insert_VUSADA' in schema 'SolucionHabitacional'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'SolucionHabitacional'
    AND SPECIFIC_NAME = N'Insert_VUSADA'
)
DROP PROCEDURE SolucionHabitacional.Insert_VUSADA
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE SolucionHabitacional.Insert_VUSADA
    @vivienda INT NOT NULL,
    @precio_final DECIMAL (18, 4) NOT NULL,
    @contribucion DECIMAL (18, 4)
AS
SET NOCOUNT ON;

-- Insert rows into table 'VUSADA'
INSERT INTO VUSADA
    ( -- columns to insert data into
    [vivienda], [precio_final], [contribucion]
    )
VALUES
    ( -- first row: values for the columns in the list above
        vivienda, precio_final, @contribucion
    )
    GO

SELECT CAST(SCOPE_IDENTITY() AS NVARCHAR);

GO

-- Create a new stored procedure called 'Select_Pasante' in schema 'SolucionHabitacional'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'SolucionHabitacional'
    AND SPECIFIC_NAME = N'Select_Pasante'
)
DROP PROCEDURE SolucionHabitacional.Select_Pasante
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE SolucionHabitacional.Select_Pasante
    @userName [NVARCHAR] (254) = NULL
AS
SET NOCOUNT ON;
IF (@userName = NULL)
    BEGIN
    -- Select rows from a Table or View 'PASANTE' in schema 'SolucionHabitacional'
    SELECT *
    FROM SolucionHabitacional.PASANTE;
END
    ELSE BEGIN
    -- Select rows from a Table or View 'USER_SYSTEM' in schema 'SolucionHabitacional'
    SELECT *
    FROM SolucionHabitacional.PASANTE
    WHERE userName = @userName;
END
    
GO

-- Create a new stored procedure called 'Select_Barrio' in schema 'SolucionHabitacional'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'SolucionHabitacional'
    AND SPECIFIC_NAME = N'Select_Barrio'
)
DROP PROCEDURE SolucionHabitacional.Select_Barrio
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE SolucionHabitacional.Select_Barrio
    @nombre [NVARCHAR] (254) = NULL
AS
    SET NOCOUNT ON;
    
    IF (@nombre = NULL)
        BEGIN
        -- Select rows from a Table or View 'BARRIO' in schema 'SolucionHabitacional'
        SELECT *
        FROM SolucionHabitacional.BARRIO;
    END
    ELSE BEGIN
        -- Select rows from a Table or View 'USER_SYSTEM' in schema 'SolucionHabitacional'
        SELECT *
        FROM SolucionHabitacional.BARRIO
        WHERE nombre = @nombre;
    END
    GO    
GO