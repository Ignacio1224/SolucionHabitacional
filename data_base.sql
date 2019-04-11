/*
* @Author: Ignacio Cabrera
* 03/2019
*/


/* DDL */
-- Create a new database called 'Solucion_Habitacional'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
FROM sys.databases
WHERE name = N'Solucion_Habitacional'
)
CREATE DATABASE Solucion_Habitacional
GO

USE Solucion_Habitacional;
GO

-- Create a new table called 'PASANTE'
-- Drop the table if it already exists
IF OBJECT_ID('PASANTE', 'U') IS NOT NULL
DROP TABLE PASANTE
GO
-- Create the table in the specified schema
CREATE TABLE PASANTE
(
    userName [NVARCHAR] (254) NOT NULL,
    -- Email
    userPassword [VARBINARY] (128) NOT NULL,

    CONSTRAINT pk_PASANTE PRIMARY KEY (userName)
);
GO

-- Create a new table called 'PARAMETRO'
-- Drop the table if it already exists
IF OBJECT_ID('PARAMETRO', 'U') IS NOT NULL
DROP TABLE PARAMETRO
GO
-- Create the table in the specified schema
CREATE TABLE PARAMETRO
(
    nombre [NVARCHAR] (254) NOT NULL,
    valor [NVARCHAR] (254) NOT NULL,

    CONSTRAINT pk_PARAMETRO PRIMARY KEY (nombre)
);
GO

-- Create a new table called 'BARRIO'
-- Drop the table if it already exists
IF OBJECT_ID('BARRIO', 'U') IS NOT NULL
DROP TABLE BARRIO
GO
-- Create the table in the specified schema
CREATE TABLE BARRIO
(
    nombre [NVARCHAR] (254),
    descripcion [NVARCHAR] (254) NOT NULL,

    CONSTRAINT pk_BARRIO PRIMARY KEY (nombre),
);
GO

-- Create a new table called 'VIVIENDA'
-- Drop the table if it already exists
IF OBJECT_ID('VIVIENDA', 'U') IS NOT NULL
DROP TABLE VIVIENDA
GO
-- Create the table in the specified schema
CREATE TABLE VIVIENDA
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

-- Create a new table called 'VNUEVA'
-- Drop the table if it already exists
IF OBJECT_ID('VNUEVA', 'U') IS NOT NULL
DROP TABLE VNUEVA
GO
-- Create the table in the specified schema
CREATE TABLE VNUEVA
(
    vivienda INT NOT NULL,
    precio_final DECIMAL (18, 4) NOT NULL

        CONSTRAINT pk_VNUEVA PRIMARY KEY (vivienda),
    CONSTRAINT fk_vivienda_VNUEVA FOREIGN KEY (vivienda) REFERENCES VIVIENDA (id)
);
GO

-- Create a new table called 'VUSADA'
-- Drop the table if it already exists
IF OBJECT_ID('VUSADA', 'U') IS NOT NULL
DROP TABLE VUSADA
GO
-- Create the table in the specified schema
CREATE TABLE VUSADA
(
    vivienda INT NOT NULL,
    precio_final DECIMAL (18, 4) NOT NULL,
    contribucion DECIMAL (18, 4),

    CONSTRAINT pk_VUSADA PRIMARY KEY (vivienda),
    CONSTRAINT fk_vivienda_VUSADA FOREIGN KEY (vivienda) REFERENCES VIVIENDA (id)
);
GO


/* STORED PROCEDURES */
USE Solucion_Habitacional;
GO
-- Create a new stored procedure called 'Insert_Pasante'
-- Drop the stored procedure if it already exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional' AND SPECIFIC_NAME = N'Insert_Pasante')
	DROP PROCEDURE Insert_Pasante;
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Insert_Pasante
    @userName [NVARCHAR] (254) = NULL,
    @userPassword [NVARCHAR] (254) = NULL
AS
BEGIN
	DECLARE @inserted INT = -1;
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM PASANTE WHERE userName = @userName) 
	BEGIN
		BEGIN TRANSACTION;
		-- Insert rows into table 'PASANTE'
		INSERT INTO PASANTE([userName], [userPassword]) VALUES(@userName, PWDENCRYPT(@userPassword));
		SELECT @inserted = @@ERROR;
		IF @inserted = 0
		BEGIN
			COMMIT TRANSACTION;
			SET @inserted = 1;
		END
		ELSE
		BEGIN
			ROLLBACK TRANSACTION;
			SET @inserted = 0;
		END
    END
	ELSE
	BEGIN
		SET @inserted = 0;
	END

	SELECT @inserted;

END
GO


-- Create a new stored procedure called 'Ingreso_Pasante'
-- Drop the stored procedure if it already exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional' AND SPECIFIC_NAME = N'Ingreso_Pasante')
	DROP PROCEDURE Ingreso_Pasante;
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Ingreso_Pasante
    @userName [NVARCHAR] (254) = NULL,
    @userPassword [NVARCHAR] (254) = NULL
AS
BEGIN
	DECLARE @ingresa INT = -1;
	DECLARE @error INT = -1;
	SET NOCOUNT ON;

	IF EXISTS (SELECT userName FROM PASANTE WHERE userName = @userName)
	BEGIN
		BEGIN TRANSACTION;
		SET @ingresa = (SELECT COUNT (userName) FROM PASANTE WHERE userName = @username AND PWDCOMPARE(@userPassword, userPassword) = 1);
		SET @error = @@ERROR;
		
		IF @error = 0 AND @ingresa = 1
		BEGIN
			COMMIT TRANSACTION;
		END
		ELSE
		BEGIN
			ROLLBACK TRANSACTION;
		END
	END
	ELSE
	BEGIN
		SET @ingresa = 0;
	END

	SELECT @ingresa;

END
GO


-- Create a new stored procedure called 'Update_Pasante'
-- Drop the stored procedure if it already exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional' AND SPECIFIC_NAME = N'Update_Pasante')
	DROP PROCEDURE Update_Pasante;
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Update_Pasante
    @userName [NVARCHAR] (254) = NULL,
    @userPassword [NVARCHAR] (254) = NULL
AS
BEGIN
	DECLARE @updated INT = -1;
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM PASANTE WHERE userName = @userName) 
	BEGIN
		BEGIN TRANSACTION;
		-- Insert rows into table 'PASANTE'
		UPDATE PASANTE SET userPassword = PWDENCRYPT(@userPassword) WHERE userName = @userName;
		SET @updated = @@ERROR
		IF @updated = 0
		BEGIN
			COMMIT TRANSACTION;
			SET @updated = 1;
		END
		ELSE
		BEGIN
			ROLLBACK TRANSACTION;
			SET @updated = 0;
		END
    END
	ELSE
	BEGIN
		SET @updated = 0;
	END

	SELECT @updated;
END
GO


-- Create a new stored procedure called 'Delete_Pasante'
-- Drop the stored procedure if it already exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional' AND SPECIFIC_NAME = N'Delete_Pasante')
	DROP PROCEDURE Delete_Pasante;
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Delete_Pasante
    @userName [NVARCHAR] (254) = NULL,
	@userPassword [NVARCHAR] (254) = NULL
AS
BEGIN
	DECLARE @error INT = -1;
	DECLARE @deleted INT = -1;
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM PASANTE WHERE userName = @userName) 
	BEGIN
		BEGIN TRANSACTION;
		-- Insert rows into table 'PASANTE'
		DELETE FROM PASANTE WHERE userName = @userName AND PWDCOMPARE(@userPassword, userPassword) = 1;
		SET @deleted = (SELECT COUNT (userName) FROM PASANTE WHERE userName = @userName);
		SET @error = @@ERROR
		
		IF @error = 0 AND @deleted = 0
		BEGIN
			COMMIT TRANSACTION;
			SET @deleted = 1;
		END
		ELSE
		BEGIN
			ROLLBACK TRANSACTION;
			SET @deleted = 0;
		END
    END
	ELSE
	BEGIN
		SET @deleted = 0;
	END

	SELECT @deleted;
END
GO


-- Create a new stored procedure called 'Insert_Parametro'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional'
    AND SPECIFIC_NAME = N'Insert_Parametro'
)
DROP PROCEDURE Insert_Parametro
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Insert_Parametro
    @nombre [NVARCHAR] (254) = NULL,
    @valor [NVARCHAR] (254) = NULL
AS
SET NOCOUNT ON;
BEGIN
	DECLARE @inserted INT = -1;
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM PARAMETRO WHERE nombre = @nombre)
	BEGIN
		BEGIN TRANSACTION
		
		-- Insert rows into table 'PARAMETRO'
		INSERT INTO PARAMETRO ([nombre], [valor]) VALUES (@nombre, @valor);
		
		SELECT @inserted = @@ERROR;
		
		IF @inserted = 0
		BEGIN
			COMMIT TRANSACTION;
			SET @inserted = 1;
		END
		ELSE
		BEGIN
			ROLLBACK TRANSACTION;
			SET @inserted = 0;
		END
    END
	ELSE
	BEGIN
		SET @inserted = 0;
	END

	SELECT @inserted;
END
GO


-- Create a new stored procedure called 'Delete_Parametro'
-- Drop the stored procedure if it already exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional' AND SPECIFIC_NAME = N'Delete_Parametro')
	DROP PROCEDURE Delete_Parametro;
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Delete_Parametro
    @nombre [NVARCHAR] (254) = NULL
AS
BEGIN
	DECLARE @error INT = -1;
	DECLARE @deleted INT = -1;
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM PARAMETRO WHERE nombre = @nombre) 
	BEGIN
		BEGIN TRANSACTION;
		-- Insert rows into table 'PASANTE'
		DELETE FROM PARAMETRO WHERE nombre = @nombre;
		SET @deleted = (SELECT COUNT (nombre) FROM PARAMETRO WHERE nombre = @nombre);
		SET @error = @@ERROR
		
		IF @error = 0 AND @deleted = 0
		BEGIN
			COMMIT TRANSACTION;
			SET @deleted = 1;
		END
		ELSE
		BEGIN
			ROLLBACK TRANSACTION;
			SET @deleted = 0;
		END
    END
	ELSE
	BEGIN
		SET @deleted = 0;
	END

	SELECT @deleted;
END
GO


-- Create a new stored procedure called 'Update_Parametro'
-- Drop the stored procedure if it already exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional' AND SPECIFIC_NAME = N'Update_Parametro')
	DROP PROCEDURE Update_Parametro;
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Update_Parametro
    @nombre [NVARCHAR] (254) = NULL,
    @valor [NVARCHAR] (254) = NULL
AS
BEGIN
	DECLARE @updated INT = -1;
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM PARAMETRO WHERE nombre = @nombre) 
	BEGIN
		BEGIN TRANSACTION;
		-- Insert rows into table 'PASANTE'
		UPDATE PARAMETRO SET valor = @valor WHERE @nombre = @nombre;
		SET @updated = @@ERROR
		IF @updated = 0
		BEGIN
			COMMIT TRANSACTION;
			SET @updated = 1;
		END
		ELSE
		BEGIN
			ROLLBACK TRANSACTION;
			SET @updated = 0;
		END
    END
	ELSE
	BEGIN
		SET @updated = 0;
	END

	SELECT @updated;
END
GO


-- Create a new stored procedure called 'Insert_Barrio'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional'
    AND SPECIFIC_NAME = N'Insert_Barrio'
)
DROP PROCEDURE Insert_Barrio
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Insert_Barrio
    @nombre [NVARCHAR] (254) = NULL,
    @descripcion [NVARCHAR] (254) = NULL
AS
BEGIN
	DECLARE @inserted INT = -1;
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM BARRIO WHERE nombre = @nombre)
	BEGIN
		BEGIN TRANSACTION
		
		-- Insert rows into table 'PARAMETRO'
		INSERT INTO BARRIO ([nombre], [descripcion]) VALUES (@nombre, @descripcion);
		
		SELECT @inserted = @@ERROR;
		
		IF @inserted = 0
		BEGIN
			COMMIT TRANSACTION;
			SET @inserted = 1;
		END
		ELSE
		BEGIN
			ROLLBACK TRANSACTION;
			SET @inserted = 0;
		END
    END
	ELSE
	BEGIN
		SET @inserted = 0;
	END

	SELECT @inserted;
END
GO

-- Create a new stored procedure called 'Delete_Barrio'
-- Drop the stored procedure if it already exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional' AND SPECIFIC_NAME = N'Delete_Barrio')
	DROP PROCEDURE Delete_Barrio;
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Delete_Barrio
    @nombre [NVARCHAR] (254) = NULL
AS
BEGIN
	DECLARE @error INT = -1;
	DECLARE @deleted INT = -1;
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM BARRIO WHERE nombre = @nombre) 
	BEGIN
		BEGIN TRANSACTION;
		-- Insert rows into table 'PASANTE'
		DELETE FROM BARRIO WHERE nombre = @nombre;
		SET @deleted = (SELECT COUNT (nombre) FROM BARRIO WHERE nombre = @nombre);
		SET @error = @@ERROR
		
		IF @error = 0 AND @deleted = 0
		BEGIN
			COMMIT TRANSACTION;
			SET @deleted = 1;
		END
		ELSE
		BEGIN
			ROLLBACK TRANSACTION;
			SET @deleted = 0;
		END
    END
	ELSE
	BEGIN
		SET @deleted = 0;
	END

	SELECT @deleted;
END
GO

-- Create a new stored procedure called 'Update_Barrio'
-- Drop the stored procedure if it already exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional' AND SPECIFIC_NAME = N'Update_Barrio')
	DROP PROCEDURE Update_Barrio;
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Update_Barrio
    @nombre [NVARCHAR] (254) = NULL,
    @descripcion [NVARCHAR] (254) = NULL
AS
BEGIN
	DECLARE @updated INT = -1;
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM BARRIO WHERE nombre = @nombre) 
	BEGIN
		BEGIN TRANSACTION;
		-- Insert rows into table 'PASANTE'
		UPDATE BARRIO SET descripcion = @descripcion WHERE nombre = @nombre;
		SET @updated = @@ERROR
		IF @updated = 0
		BEGIN
			COMMIT TRANSACTION;
			SET @updated = 1;
		END
		ELSE
		BEGIN
			ROLLBACK TRANSACTION;
			SET @updated = 0;
		END
    END
	ELSE
	BEGIN
		SET @updated = 0;
	END

	SELECT @updated;
END
GO

-- Create a new stored procedure called 'Insert_Vivienda'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional'
    AND SPECIFIC_NAME = N'Insert_Vivienda'
)
DROP PROCEDURE Insert_Vivienda
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Insert_Vivienda
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

-- Create a new stored procedure called 'Insert_VNUEVA'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional'
    AND SPECIFIC_NAME = N'Insert_VNUEVA'
)
DROP PROCEDURE Insert_VNUEVA
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Insert_VNUEVA
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


-- Create a new stored procedure called 'Insert_VUSADA'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional'
    AND SPECIFIC_NAME = N'Insert_VUSADA'
)
DROP PROCEDURE Insert_VUSADA
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Insert_VUSADA
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

-- Create a new stored procedure called 'Select_Pasante'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional'
    AND SPECIFIC_NAME = N'Select_Pasante'
)
DROP PROCEDURE Select_Pasante
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Select_Pasante
    @userName [NVARCHAR] (254) = NULL
AS
SET NOCOUNT ON;
IF (@userName = NULL)
    BEGIN
    -- Select rows from a Table or View 'PASANTE'
    SELECT *
    FROM PASANTE;
END
    ELSE BEGIN
    -- Select rows from a Table or View 'USER_SYSTEM'
    SELECT *
    FROM PASANTE
    WHERE userName = @userName;
END
    
GO

-- Create a new stored procedure called 'Select_Barrio'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'Solucion_Habitacional'
    AND SPECIFIC_NAME = N'Select_Barrio'
)
DROP PROCEDURE Select_Barrio
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE Select_Barrio
    @nombre [NVARCHAR] (254) = NULL
AS
    SET NOCOUNT ON;
    
    IF (@nombre = NULL)
        BEGIN
        -- Select rows from a Table or View 'BARRIO'
        SELECT *
        FROM BARRIO;
    END
    ELSE BEGIN
        -- Select rows from a Table or View 'USER_SYSTEM'
        SELECT *
        FROM BARRIO
        WHERE nombre = @nombre;
    END
    GO    
GO