CREATE DATABASE maritimecargotransportationdb;

\c maritimecargotransportationdb;

CREATE TABLE users
(
    id         serial primary key,
    first_name varchar(50)        not null,
    last_name  varchar(50)        not null,
    email      varchar(50) unique not null,
    password   varchar(50)        not null
);

CREATE TABLE Companies
(
    id      serial primary key,
    user_id int                not null references users (id),
    name    varchar(50) unique not null
);

CREATE TABLE ship_types
(
    id   serial primary key,
    name varchar(50) unique not null
);

CREATE TABLE flags
(
    id   serial primary key,
    name varchar(50) unique not null
);

CREATE TABLE ships
(
    id           serial primary key,
    name         varchar(50) unique not null,
    flag_id      int                not null references Flags (id),
    ship_type_id int                not null references ShipTypes (id),
    company_id   int                not null references Companies (id)
);

CREATE TABLE ContainerShipSizeTypes
(
    id   serial primary key,
    name varchar(50) unique not null
);

CREATE TABLE ContainerTypes
(
    id   serial primary key,
    name varchar(50) unique not null
);

CREATE TABLE CargoTypes
(
    id   serial primary key,
    name varchar(50) unique not null
);


CREATE TABLE Cargoes
(
    id          serial primary key,
    cargoTypeId int not null references CargoTypes (id)
);

CREATE TABLE ContainerShip
(
    id     serial primary key,
    shipId int not null references Ships (id),
    sizeId int not null references ContainerShipSizeTypes (id)
);

CREATE TABLE Containers
(
    id              serial primary key,
    containerShipId int not null references ContainerShip (id),
    containerTypeId int not null references ContainerTypes (id),
    cargoId         int not null references Cargoes (id)
);

-- seed

insert into Users (firstname, lastname, email, password)
values ('Natala', 'Alessandretti', 'nalessandretti0@godaddy.com', 'iK2#|c(HzMJ');
insert into Users (firstname, lastname, email, password)
values ('Ronny', 'Wishart', 'rwishart1@state.tx.us', 'sY3*<SP(meGE');
insert into Users (firstname, lastname, email, password)
values ('Rhody', 'Mardling', 'rmardling2@purevolume.com', 'mC6_%?(y');
insert into Users (firstname, lastname, email, password)
values ('Beaufort', 'Emmett', 'bemmett3@360.cn', 'fF2(|AHqvu''%oK');
insert into Users (firstname, lastname, email, password)
values ('Colman', 'Cluitt', 'ccluitt4@blog.com', 'qB5{|3,)#qk''PE1');
insert into Users (firstname, lastname, email, password)
values ('Nalani', 'Danniell', 'ndanniell5@unblog.fr', 'zI7.5J_K,w\F');
insert into Users (firstname, lastname, email, password)
values ('Brock', 'Sayers', 'bsayers6@unc.edu', 'mU3~Iv)KNq@jxLLF');
insert into Users (firstname, lastname, email, password)
values ('Hillyer', 'Kale', 'hkale7@utexas.edu', 'lU0&G_BvYc7"E');
insert into Users (firstname, lastname, email, password)
values ('Langston', 'Brewitt', 'lbrewitt8@census.gov', 'uV2@"jX''A');
insert into Users (firstname, lastname, email, password)
values ('Dulce', 'Edyson', 'dedyson9@mysql.com', 'yZ0=)mC@g<kWW');

insert into Companies (userid, name)
values (1, 'JumpXS');
insert into Companies (userid, name)
values (2, 'Kwinu');
insert into Companies (userid, name)
values (3, 'Edgeblab');
insert into Companies (userid, name)
values (4, 'Kazio');
insert into Companies (userid, name)
values (5, 'Skidoo');
insert into Companies (userid, name)
values (6, 'Realcube');
insert into Companies (userid, name)
values (7, 'Meevee');
insert into Companies (userid, name)
values (9, 'Realcube');
insert into Companies (userid, name)
values (1, 'Thoughtblab');
insert into Companies (userid, name)
values (2, 'Vinder');
insert into Companies (userid, name)
values (3, 'Eamia');
insert into Companies (userid, name)
values (4, 'Realmix');
insert into Companies (userid, name)
values (5, 'Realmix');
insert into Companies (userid, name)
values (6, 'Zooveo');
insert into Companies (userid, name)
values (7, 'Blogtags');
insert into Companies (userid, name)
values (8, 'Brightbean');
insert into Companies (userid, name)
values (9, 'Eamia');
insert into Companies (userid, name)
values (1, 'Eazzy');
insert into Companies (userid, name)
values (2, 'Quinu');
insert into Companies (userid, name)
values (3, 'Jaloo');
insert into Companies (userid, name)
values (4, 'Layo');
insert into Companies (userid, name)
values (5, 'Yodel');
insert into Companies (userid, name)
values (6, 'Linktype');
insert into Companies (userid, name)
values (7, 'Yambee');
insert into Companies (userid, name)
values (8, 'Meembee');
insert into Companies (userid, name)
values (9, 'Kamba');


INSERT INTO Flags (name)
values ('BERMUDA');

INSERT INTO ShipTypes (name)
values ('Container ship'),
       ('Tanker'),
       ('Roll-on/Roll-off vessel'),
       ('Dry bulk carrier'),
       ('Multi-purpose vessel');

INSERT INTO CargoTypes (name)
values ('Bauxite'),
       ('Bulk minerals'),
       ('Cements'),
       ('Chemicals'),
       ('Coals and cokes'),
       ('Agricultural products'),
       ('Iron'),
       ('Wood chips'),
       ('Hazardous chemicals in liquid form'),
       ('Petroleum'),
       ('Gasoline'),
       ('Liquifiend natual gas (LNG)'),
       ('Liquid nitrogen'),
       ('Cooking oil'),
       ('Vegetable oil'),
       ('Fruit juices'),
       ('Rubber');

INSERT INTO ContainerShipSizeTypes (name)
values ('Ultra Large Container Vessel (ULCV)'),
       ('New Panamax'),
       ('Post-Panamax'),
       ('Panamax'),
       ('Feedermax'),
       ('Feeder'),
       ('Small feeder');

INSERT INTO ContainerTypes (name)
values ('General-purpose dry van'),
       ('Ventilated container'),
       ('Temperature controlled container'),
       ('Tank container'),
       ('Open-top / open-side container'),
       ('Log cradle'),
       ('Trash container');

INSERT INTO Ships (name, ownerCompanyId, flagId, shipTypeId)
values ('initial-ship', '1', '1', '1');
