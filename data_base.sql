/*
* @Author: Ignacio Cabrera - Matias Schmid
* 03/2019
*/


/* DDL */
CREATE DATABASE SolucionHabitacional;

USE SolucionHabitacional;

CREATE TABLE USER_SYSTEM (
    documento VARCHAR (24),
    nombre VARCHAR (254) NOT NULL,
    apellido VARCHAR (254) NOT NULL,

    CONSTRAINT pk_USER_SYSTEM PRIMARY KEY (documento)
);

CREATE TABLE PASANTE (
    documento VARCHAR (24),
    user_name VARCHAR (254) NOT NULL,
    user_password VARCHAR (254) NOT NULL,

    CONSTRAINT pk_PASANTE PRIMARY KEY (documento),
    CONSTRAINT fk_documento_PASANTE FOREIGN KEY (documento) REFERENCES USER_SYSTEM (documento),
    CONSTRAINT un_userName_PASANTE UNIQUE (user_name)
);

CREATE TABLE PARAMETRO (
    nombre VARCHAR (254),
    fecha_vigencia DATETIME2,
    valor VARCHAR (254) NOT NULL,

    CONSTRAINT pk_PARAMETRO PRIMARY KEY (nombre, fecha_vigencia),
);

CREATE TABLE BARRIO (
    nombre VARCHAR (254),
    descripcion VARCHAR (254) NOT NULL,

    CONSTRAINT pk_BARRIO PRIMARY KEY (nombre),
);

CREATE TABLE VIVIENDA (
    id INT IDENTITY (1, 1),
    calle VARCHAR (254) NOT NULL,
    nro_puerta INT NOT NULL,
    descripcion VARCHAR (254),
    nro_banios INT NOT NULL,
    nro_dormitorios INT NOT NULL,
    superficie DECIMAL (18, 4) NOT NULL,
    anio_construccion INT,
    precio_base DECIMAL (18, 4) NOT NULL,
    cotizacion DECIMAL (18, 4) NOT NULL,
    precio_final DECIMAL (18, 4) NOT NULL,
    habilitada CHAR (1) NOT NULL,
    vendida CHAR (1) NOT NULL,
    barrio VARCHAR (254) NOT NULL,

    CONSTRAINT pk_VIVIENDA PRIMARY KEY (id),
    CONSTRAINT fk_barrio_VIVIENDA FOREIGN KEY (barrio) REFERENCES BARRIO (nombre),
    CONSTRAINT un_calle_nroPuerta_PASANTE UNIQUE (calle, nro_puerta)
);

CREATE TABLE VNUEVA (
    vivienda INT,
    superficie_max DECIMAL (18, 4) NOT NULL, /* ESTOY EN DUDA*/
    tiempo_financiacion INT,

    CONSTRAINT pk_VNUEVA PRIMARY KEY (vivienda), 
    CONSTRAINT fk_vivienda_VIVIENDA FOREIGN KEY (vivienda) REFERENCES VIVIENDA (id)   
);

CREATE TABLE VUSADA (
    vivienda INT,
    impuesto_transacciones_patrimoniales DECIMAL (18, 4) NOT NULL, /* ESTOY EN DUDA*/
    tiempo_financiacion INT,
    contribucion DECIMAL (18, 4),

    CONSTRAINT pk_VNUEVA PRIMARY KEY (vivienda), 
    CONSTRAINT fk_vivienda_VIVIENDA FOREIGN KEY (vivienda) REFERENCES VIVIENDA (id)   
);


/* STORE PROCEDURES*/
