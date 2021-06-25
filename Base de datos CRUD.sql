create database CRUD
use CRUD


create table Provincia(
ProvinciaId int not null identity(1,1) primary key,
NombreProvincia varchar(255) not null
)

create table Personas(
PersonAId int not null identity(1,1) primary key,
Nombre varchar(200) not null,
Apellido varchar(200) not null,
IdProvincia int not null,
 foreign key (IdProvincia) references Provincia(ProvinciaId)
)

insert into Provincia values ('San Pedro de Macoris')
insert into Provincia values ('Samana')
insert into Provincia values ('San Francisco de Macoris')
insert into Provincia values ('La Romana')
insert into Provincia values ('Consuelo')
insert into Provincia values ('Santo Domingo')


create procedure InsertarPersona
(@nombre varchar(200), @apellido varchar(200), @Idprovincia int)
as begin
set nocount on
insert into Personas(Nombre, Apellido, IdProvincia) values (@nombre, @apellido,@Idprovincia)
end
insert into Personas (Nombre,Apellido,IdProvincia) values ('Eduardo','Alcantara',1)
insert into Personas (Nombre,Apellido,IdProvincia) values ('Joan','Berroa',2)
insert into Personas (Nombre,Apellido,IdProvincia) values ('Juan','Martinez',3)
insert into Personas (Nombre,Apellido,IdProvincia) values ('Raul','Perez',4)



create procedure EliminarPersona
(@IdPersona int)
as begin
set nocount on;
	if @IdPersona is null return 1;
	if @IdPersona =0 return 1;
	if @IdPersona >0 begin
	delete from Personas where PersonAId =@IdPersona

	end
end

create procedure EditarPersona
(@IdPersona int, @nombre varchar(200),@Apellido varchar(200), @provincia int)
as begin
	if @IdPersona is null return 1;
	if @IdPersona =0 return 1;
	if @provincia is null return 1;
	if @provincia =0 return 1;
	update Personas set Nombre =@nombre, Apellido=@Apellido, IdProvincia=@provincia where PersonAId=@IdPersona;
end

create procedure ObtenerPersonas
as begin
set nocount on;
select p.PersonAId,p.Nombre, p.Apellido, pr.NombreProvincia, pr.ProvinciaId  from Personas
 p inner join Provincia pr on p.IdProvincia= pr.ProvinciaId 
end

create procedure ObtenerProvincias
as begin
select * from Provincia
end

create procedure ObtenerPersonaPorId
(@IdPersona int)
as begin
set nocount on;
if @IdPersona is null return 1;
	select p.PersonAId,p.Nombre, p.Apellido from Personas p where p.PersonAId = @IdPersona;
end

 
