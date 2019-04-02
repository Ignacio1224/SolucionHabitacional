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

-- Create a new table called 'USER_SYSTEM' in schema 'SolucionHabitacional'
-- Drop the table if it already exists
IF OBJECT_ID('SolucionHabitacional.USER_SYSTEM', 'U') IS NOT NULL
DROP TABLE SolucionHabitacional.USER_SYSTEM
GO
-- Create the table in the specified schema
CREATE TABLE SolucionHabitacional.USER_SYSTEM
(
    documento [NVARCHAR] (24) NOT NULL,
    nombre [NVARCHAR](254) NOT NULL,
    apellido [NVARCHAR](254) NOT NULL

    CONSTRAINT pk_USER_SYSTEM PRIMARY KEY (documento)
);
GO

-- Create a new table called 'PASANTE' in schema 'SolucionHabitacional'
-- Drop the table if it already exists
IF OBJECT_ID('SolucionHabitacional.PASANTE', 'U') IS NOT NULL
DROP TABLE SolucionHabitacional.PASANTE
GO
-- Create the table in the specified schema
CREATE TABLE SolucionHabitacional.PASANTE
(
    documento [NVARCHAR] (24) NOT NULL,
    userName [NVARCHAR] (254) NOT NULL,
    userPassword [NVARCHAR] (254) NOT NULL,

    CONSTRAINT pk_PASANTE PRIMARY KEY (documento),
    CONSTRAINT fk_documento_PASANTE FOREIGN KEY (documento) REFERENCES USER_SYSTEM (documento),
    CONSTRAINT un_userName_PASANTE UNIQUE (userName)
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
    fecha_vigencia DATETIME2 NOT NULL,
    valor [NVARCHAR] (254) NOT NULL,

    CONSTRAINT pk_PARAMETRO PRIMARY KEY (nombre, fecha_vigencia)
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
    cotizacion DECIMAL (18, 4) NOT NULL,
    precio_final DECIMAL (18, 4) NOT NULL,
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
    superficie_max DECIMAL (18, 4) NOT NULL,
    /* ESTOY EN DUDA*/
    tiempo_financiacion INT,

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
    impuesto_transacciones_patrimoniales DECIMAL (18, 4) NOT NULL,
    /* ESTOY EN DUDA*/
    tiempo_financiacion INT,
    contribucion DECIMAL (18, 4),

    CONSTRAINT pk_VNUEVA PRIMARY KEY (vivienda),
    CONSTRAINT fk_vivienda_VIVIENDA FOREIGN KEY (vivienda) REFERENCES VIVIENDA (id)
);
GO


/* STORED PROCEDURES */

-- Create a new stored procedure called 'Insert_User_System' in schema 'SolucionHabitacional'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'SolucionHabitacional'
    AND SPECIFIC_NAME = N'Insert_User_System'
)
DROP PROCEDURE SolucionHabitacional.Insert_User_System
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE SolucionHabitacional.Insert_User_System
    @documento [NVARCHAR] (24) = NULL,
    @nombre [NVARCHAR] (254) = NULL, 
    @apellido [NVARCHAR] (254) = NULL
AS
    SET NOCOUNT ON;
    
    -- Insert rows into table 'USER_SYSTEM'
    INSERT INTO USER_SYSTEM
    ( -- columns to insert data into
     [documento], [nombre], [apellido]
    )
    VALUES
    ( -- first row: values for the columns in the list above
     @documento, @nombre, @apellido
    )
    GO
    
    SELECT CAST(SCOPE_IDENTITY() AS NVARCHAR);

GO

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
    @documento [NVARCHAR] (24) = NULL,
    @userName [NVARCHAR] (254) = NULL,
    @userPassword [NVARCHAR] (254) = NULL
AS
    SET NOCOUNT ON;

    -- Insert rows into table 'PASANTE'
    INSERT INTO PASANTE
    ( -- columns to insert data into
     [documento], [userName], [userPassword]
    )
    VALUES
    ( -- first row: values for the columns in the list above
     @documento, @userName, @userPassword
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
    @valor [NVARCHAR] (254) = NULL, 
    @fecha_vigencia DATETIME2 = NULL
AS
    SET NOCOUNT ON;

    -- Insert rows into table 'PARAMETRO'
    INSERT INTO PARAMETRO
    ( -- columns to insert data into
     [nombre], [valor], [fecha_vigencia]
    )
    VALUES
    ( -- first row: values for the columns in the list above
     @nombre, @valor, GETDATE()
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
    @cotizacion DECIMAL (18, 4) = NULL,
    @precio_final DECIMAL (18, 4) = NULL,
    @habilitada CHAR (1) = NULL,
    @vendida CHAR (1) = NULL,
    @barrio [NVARCHAR] (254) = NULL
AS
    SET NOCOUNT ON;
    
    -- Insert rows into table 'VIVIENDA'
    INSERT INTO VIVIENDA
    ( -- columns to insert data into
     [calle], [nro_puerta], [descripcion], [nro_banios], [nro_dormitorios], [superficie], [anio_construccion], [precio_base], [cotizacion], [precio_final], [habilitada], [vendida], [barrio]
    )
    VALUES
    ( -- first row: values for the columns in the list above
     @calle, @nro_puerta, @descripcion, @nro_banios, @nro_dormitorios, @superficie, @anio_construccion, @precio_base,@cotizacion, @precio_final, @habilitada, @vendida, @barrio
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
    
AS
    SET NOCOUNT ON;

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

AS
    SET NOCOUNT ON;

    SELECT CAST(SCOPE_IDENTITY() AS NVARCHAR);

GO

-- Create a new stored procedure called 'Select_UserSystem' in schema 'SolucionHabitacional'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'SolucionHabitacional'
    AND SPECIFIC_NAME = N'Select_UserSystem'
)
DROP PROCEDURE SolucionHabitacional.Select_UserSystem
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE SolucionHabitacional.Select_UserSystem
    @documento [NVARCHAR] (24) = NULL
AS
    SET NOCOUNT ON;
    IF (@documento = NULL)
    BEGIN
        -- Select rows from a Table or View 'USER_SYSTEM' in schema 'SolucionHabitacional'
        SELECT * FROM SolucionHabitacional.USER_SYSTEM;
    END
    ELSE BEGIN
        -- Select rows from a Table or View 'USER_SYSTEM' in schema 'SolucionHabitacional'
        SELECT * FROM SolucionHabitacional.USER_SYSTEM
        WHERE documento = @documento;
    END
    
GO
