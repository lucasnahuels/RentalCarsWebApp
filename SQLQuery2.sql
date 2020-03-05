SET DATEFORMAT dmy

use BDDAlquilerAutos


CREATE TABLE oficina
	(
	codigo_unico_oficina int primary key identity(1,1) NOT NULL,
	calle char(50) NOT NULL,
	numero int NOT NULL,
	telefono char(50) NOT NULL,
	id_ciudad int NOT NULL,
	foreign key(id_ciudad)
	references ciudad(id_ciudad)
	on update cascade on  delete no action	
	)  



CREATE TABLE ciudad
	(
	id_ciudad int primary key identity(1,1) NOT NULL,
	codigo_postal int NOT NULL,
	nombre_ciudad char(50) NOT NULL,
	)  
	

CREATE TABLE coche
	(
	id_coche int primary key identity(1,1) NOT NULL,
	patente char(20) NOT NULL,
	grupo char(1) NOT NULL,
	marca char(50)NOT NULL,
	modelo int NOT NULL,
	numero_puertas int NOT NULL,
	numero_pasajeros int NOT NULL,
	capacidad_baúl int NOT NULL,
	codigo_unico_oficina int NOT NULL,
	foreign key(codigo_unico_oficina)
	references oficina(codigo_unico_oficina)
	on update cascade on  delete no action	
	)  

	
CREATE TABLE alquiler
	(
	id_alquiler int primary key identity(1,1) NOT NULL,
	duracion_dias int NOT NULL,
	tipo_seguro char(50) NOT NULL,
	precio smallmoney NOT NULL,
	id_conductor int NOT NULL,
	id_coche int NOT NULL,
	foreign key(id_conductor)
	references conductor(id_conductor)
	on update cascade on  delete no action,	
	foreign key(id_coche)
	references coche(id_coche)
	on update cascade on  delete no action	
	)
	
CREATE TABLE conductor
	(
	id_conductor int primary key identity(1,1) NOT NULL,
	DNI int unique NOT NULL,
	duracion_dias int NOT NULL,
	nombre_conductor char(50) NOT NULL,
	direccion char(50),
	telefono_contacto char(20),
	nro_tarjeta_credito bigint
	)
	
alter table conductor
drop column duracion_dias


insert into ciudad(codigo_postal, nombre_ciudad)values(1684, 'El Palomar')
insert into ciudad(codigo_postal, nombre_ciudad)values(1200, 'Haedo')
insert into ciudad(codigo_postal, nombre_ciudad)values(1000, 'Moron')
insert into ciudad(codigo_postal, nombre_ciudad)values(2080, 'Ramos Mejia')

insert into oficina(calle, numero, telefono, id_ciudad)values('Noruega', 095, '11-4444-2313', 1)
insert into oficina(calle, numero, telefono, id_ciudad)values('Peron', 33, '11-3232-2222', 1)
insert into oficina(calle, numero, telefono, id_ciudad)values('Balvin', 2323, '11-4324-4423', 3)
insert into oficina(calle, numero, telefono, id_ciudad)values('Derqui', 453, '11-4324-2221', 2)

insert into conductor(DNI, duracion_dias, direccion, nombre_conductor, nro_tarjeta_credito, telefono_contacto)values('40430340', 3, 'Suecia 333', 'Juan', 1223838327233333, '11-4434-3434')
insert into conductor(DNI, duracion_dias, direccion, nombre_conductor, nro_tarjeta_credito, telefono_contacto)values('44334342', 100, 'Corrientes 323', 'Marcos', 1111222233334444, '11-3434-4444')
insert into conductor(DNI, duracion_dias, direccion, nombre_conductor, nro_tarjeta_credito, telefono_contacto)values('33300230', 18, 'Julio 23', 'Martin', 5555666677778888, '11-3434-5555')
insert into conductor(DNI, duracion_dias, direccion, nombre_conductor, nro_tarjeta_credito, telefono_contacto)values('29930948', 30, 'Cordoba 323', 'Roberto', 1000200030004000, '11-3442-3434')

insert into coche(patente, grupo, marca, modelo, numero_pasajeros, numero_puertas, codigo_unico_oficina, capacidad_baúl)values('SAD333', 'A', 'Ford', 2010, 5, 4, 1, 3000)
insert into coche(patente, grupo, marca, modelo, numero_pasajeros, numero_puertas, codigo_unico_oficina, capacidad_baúl)values('DSA212', 'B', 'Chevrolet', 2015, 2, 3, 1, 2000)
insert into coche(patente, grupo, marca, modelo, numero_pasajeros, numero_puertas, codigo_unico_oficina, capacidad_baúl)values('FGD322', 'C', 'Chevrolet', 2000, 5, 5, 2, 10000)
insert into coche(patente, grupo, marca, modelo, numero_pasajeros, numero_puertas, codigo_unico_oficina, capacidad_baúl)values('FFF333', 'D', 'Peugeot', 2018, 5, 4, 4, 5000)


insert into alquiler(id_conductor, duracion_dias, id_coche, precio, tipo_seguro)values(1, 2, 4, '4000.10', 'Todo riesgo')
insert into alquiler(id_conductor, duracion_dias, id_coche, precio, tipo_seguro)values(1, 100, 4, '40,000.50', 'Todo riesgo')
insert into alquiler(id_conductor, duracion_dias, id_coche, precio, tipo_seguro)values(3, 50, 3, '10000', 'Todo riesgo')
insert into alquiler(id_conductor, duracion_dias, id_coche, precio, tipo_seguro)values(4, 20, 2, '3000.20', 'Todo riesgo')

	
	
select* from ciudad
select* from oficina
select* from conductor
select* from coche
select* from alquiler
