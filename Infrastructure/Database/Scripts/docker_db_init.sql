CREATE TABLE users
(
    id         serial primary key,
    first_name varchar(50)        not null,
    last_name  varchar(50)        not null,
    email      varchar(50) unique not null,
    password   varchar(50)        not null
);

CREATE TABLE companies
(
    id   serial primary key,
    name varchar(50) unique not null
);

CREATE TABLE users_to_companies
(
    id         serial primary key,
    user_id    int not null references users (id),
    company_id int not null references companies (id)
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
    flag_id      int                not null references flags (id),
    ship_type_id int                not null references ship_types (id),
    company_id   int                not null references companies (id)
);

CREATE TABLE container_ship_size_types
(
    id   serial primary key,
    name varchar(50) unique not null
);

CREATE TABLE container_types
(
    id   serial primary key,
    name varchar(50) unique not null
);

CREATE TABLE cargo_types
(
    id   serial primary key,
    name varchar(50) unique not null
);

CREATE TABLE cargoes
(
    id            serial primary key,
    cargo_type_id int not null references cargo_types (id)
);

CREATE TABLE container_ships
(
    id      serial primary key,
    ship_id int not null references ships (id),
    size_id int not null references container_ship_size_types (id)
);

CREATE TABLE containers
(
    id                serial primary key,
    container_ship_id int not null references container_ships (id),
    container_type_id int not null references container_types (id)
);

CREATE TABLE cargoes_to_containers
(
    id           serial primary key,
    cargo_id     int not null references cargoes (id),
    container_id int not null references containers (id)
);

-- seed

insert into users (first_name, last_name, email, password)
values ('Natala', 'Alessandretti', 'nalessandretti0@godaddy.com', 'iK2#|c(HzMJ'),
       ('Ronny', 'Wishart', 'rwishart1@state.tx.us', 'sY3*<SP(meGE'),
       ('Rhody', 'Mardling', 'rmardling2@purevolume.com', 'mC6_%?(y'),
       ('Beaufort', 'Emmett', 'bemmett3@360.cn', 'fF2(|AHqvu''%oK'),
       ('Colman', 'Cluitt', 'ccluitt4@blog.com', 'qB5{|3,)#qk''PE1'),
       ('Nalani', 'Danniell', 'ndanniell5@unblog.fr', 'zI7.5J_K,w\F'),
       ('Brock', 'Sayers', 'bsayers6@unc.edu', 'mU3~Iv)KNq@jxLLF'),
       ('Hillyer', 'Kale', 'hkale7@utexas.edu', 'lU0&G_BvYc7"E'),
       ('Langston', 'Brewitt', 'lbrewitt8@census.gov', 'uV2@"jX''A'),
       ('Dulce', 'Edyson', 'dedyson9@mysql.com', 'yZ0=)mC@g<kWW');

insert into companies (name)
values ('JumpXS'),
       ('Thoughtblab'),
       ('Eazzy'),
       ('Kwinu'),
       ('Vinder'),
       ('Quinu'),
       ('Edgeblab'),
       ('Victory'),
       ('Jaloo'),
       ('Kazio'),
       ('Unrealmix'),
       ('Layo'),
       ('Skidoo'),
       ('Realmix'),
       ('Yodel'),
       ('Unrealcube'),
       ('Zooveo'),
       ('Linktype'),
       ('Meevee'),
       ('Blogtags'),
       ('Yambee'),
       ('Brightbean'),
       ('Meembee'),
       ('Realcube'),
       ('Eamia'),
       ('Kamba');

INSERT INTO users_to_companies (user_id, company_id)
values (1, 1),
       (1, 2),
       (1, 3),
       (2, 4),
       (2, 5),
       (2, 6),
       (3, 7),
       (3, 8),
       (3, 9),
       (4, 10),
       (4, 11),
       (4, 12),
       (5, 13),
       (5, 14),
       (5, 15),
       (6, 16),
       (6, 17),
       (6, 18),
       (7, 19),
       (7, 20),
       (7, 21),
       (8, 22),
       (8, 23),
       (9, 24),
       (9, 25),
       (9, 26);

INSERT INTO flags (name)
values ('BERMUDA');

INSERT INTO ship_types (name)
values ('Container ship'),
       ('Tanker'),
       ('Roll-on/Roll-off vessel'),
       ('Dry bulk carrier'),
       ('Multi-purpose vessel');

INSERT INTO cargo_types (name)
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

INSERT INTO container_ship_size_types (name)
values ('Ultra Large Container Vessel (ULCV)'),
       ('New Panamax'),
       ('Post-Panamax'),
       ('Panamax'),
       ('Feedermax'),
       ('Feeder'),
       ('Small feeder');

INSERT INTO container_types (name)
values ('General-purpose dry van'),
       ('Ventilated container'),
       ('Temperature controlled container'),
       ('Tank container'),
       ('Open-top / open-side container'),
       ('Log cradle'),
       ('Trash container');

INSERT INTO ships (name, company_id, flag_id, ship_type_id)
values ('initial-ship', 1, 1, 1);

INSERT INTO cargoes (cargo_type_id)
values (1),
       (2),
       (3),
       (4),
       (5),
       (6),
       (7),
       (8);

INSERT INTO container_ships (ship_id, size_id)
VALUES (1, 2);
INSERT INTO containers (container_type_id, container_ship_id)
VALUES (1, 1),
       (2, 1),
       (3, 1);

INSERT INTO cargoes_to_containers (cargo_id, container_id)
VALUES (1, 1),
       (2, 2),
       (3, 3);
