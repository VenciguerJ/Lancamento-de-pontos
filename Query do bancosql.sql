Create database Entrega;
create table Jogo(
Id int not null primary key identity,
DataJogo DATETIME2 not null,
QuantidadePontos int not null
);

create table Recordes(
Id int not null primary key identity,
DataJogo DATETIME2 not null,
QuantidadePontos int not null
);

use Entrega;
drop table Jogo;


select * from Jogo;
insert into Jogo (DataJogo, QuantidadePontos) values ('2024-12-15', 10);

SELECT * FROM information_schema.tables WHERE table_name = 'Entrega';
