CREATE TABLE users
(
    id         serial primary key,
    first_name varchar(50)        not null,
    last_name  varchar(50)        not null,
    email      varchar(50) unique not null,
    phone      varchar(50) unique not null,
    password   varchar(50)        not null
);

CREATE TABLE companies
(
    id   serial primary key,
    name varchar(50) unique not null,
    email varchar(50) unique not null,
    phone varchar(50) unique not null,
    address varchar(100),
    website varchar(50)
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

CREATE TABLE countries
(
    id   serial primary key,
    name varchar(200) unique not null,
    code varchar(2)   unique not null,
    country_code varchar(3) unique not null
);

CREATE TABLE ships
(
    id           serial primary key,
    name         varchar(50) unique not null,
    country_id      int                not null references countries (id),
    ship_type_id int                not null references ship_types (id),
    company_id   int                not null references companies (id),
    length       int                not null,
    beam         int                not null,
    draft        int                not null,
    year_built   int                not null,
    imo          varchar(50)        not null
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
    size_id int not null references container_ship_size_types (id),
    capacity int not null
);

CREATE TABLE containers
(
    id                serial primary key,
    container_ship_id int references container_ships (id),
    container_type_id int not null references container_types (id)
);

CREATE TABLE cargoes_to_containers
(
    id           serial primary key,
    cargo_id     int not null references cargoes (id),
    container_id int not null references containers (id)
);

-- seed

insert into users (first_name, last_name, email, phone, password)
values ('Root', 'User', 'admin@example.com', '9999999999', 'root'),
     ('Dud', 'Metheringham', 'dmetheringham0@etsy.com', '2539381787', 'bI6\V#U9b%~'),
     ('Lennard', 'Gentile', 'lgentile1@list-manage.com', '9985297283', 'hW9(>_aF>');

insert into companies (name, email, phone, address, website)
values ('Maritime Logistics', 'maritimelogistics@dcorp.com', '6065664001', 'PO Box 39473', 'maritimelogistics.com');


INSERT INTO users_to_companies (user_id, company_id)
values (1, 1),
       (2, 1),
       (3, 1);

INSERT INTO countries (country_code, name, code)
VALUES
    ('AFG','Afghanistan','AF'),
    ('ALA','Åland','AX'),
    ('ALB','Albania','AL'),
    ('DZA','Algeria','DZ'),
    ('ASM','American Samoa','AS'),
    ('AND','Andorra','AD'),
    ('AGO','Angola','AO'),
    ('AIA','Anguilla','AI'),
    ('ATA','Antarctica','AQ'),
    ('ATG','Antigua and Barbuda','AG'),
    ('ARG','Argentina','AR'),
    ('ARM','Armenia','AM'),
    ('ABW','Aruba','AW'),
    ('AUS','Australia','AU'),
    ('AUT','Austria','AT'),
    ('AZE','Azerbaijan','AZ'),
    ('BHS','Bahamas','BS'),
    ('BHR','Bahrain','BH'),
    ('BGD','Bangladesh','BD'),
    ('BRB','Barbados','BB'),
    ('BLR','Belarus','BY'),
    ('BEL','Belgium','BE'),
    ('BLZ','Belize','BZ'),
    ('BEN','Benin','BJ'),
    ('BMU','Bermuda','BM'),
    ('BTN','Bhutan','BT'),
    ('BOL','Bolivia','BO'),
    ('BES','Bonaire','BQ'),
    ('BIH','Bosnia and Herzegovina','BA'),
    ('BWA','Botswana','BW'),
    ('BVT','Bouvet Island','BV'),
    ('BRA','Brazil','BR'),
    ('IOT','British Indian Ocean Territory','IO'),
    ('VGB','British Virgin Islands','VG'),
    ('BRN','Brunei','BN'),
    ('BGR','Bulgaria','BG'),
    ('BFA','Burkina Faso','BF'),
    ('BDI','Burundi','BI'),
    ('KHM','Cambodia','KH'),
    ('CMR','Cameroon','CM'),
    ('CAN','Canada','CA'),
    ('CPV','Cape Verde','CV'),
    ('CYM','Cayman Islands','KY'),
    ('CAF','Central African Republic','CF'),
    ('TCD','Chad','TD'),
    ('CHL','Chile','CL'),
    ('CHN','China','CN'),
    ('CXR','Christmas Island','CX'),
    ('CCK','Cocos [Keeling] Islands','CC'),
    ('COL','Colombia','CO'),
    ('COM','Comoros','KM'),
    ('COK','Cook Islands','CK'),
    ('CRI','Costa Rica','CR'),
    ('HRV','Croatia','HR'),
    ('CUB','Cuba','CU'),
    ('CUW','Curacao','CW'),
    ('CYP','Cyprus','CY'),
    ('CZE','Czech Republic','CZ'),
    ('COD','Democratic Republic of the Congo','CD'),
    ('DNK','Denmark','DK'),
    ('DJI','Djibouti','DJ'),
    ('DMA','Dominica','DM'),
    ('DOM','Dominican Republic','DO'),
    ('TLS','East Timor','TL'),
    ('ECU','Ecuador','EC'),
    ('EGY','Egypt','EG'),
    ('SLV','El Salvador','SV'),
    ('GNQ','Equatorial Guinea','GQ'),
    ('ERI','Eritrea','ER'),
    ('EST','Estonia','EE'),
    ('ETH','Ethiopia','ET'),
    ('FLK','Falkland Islands','FK'),
    ('FRO','Faroe Islands','FO'),
    ('FJI','Fiji','FJ'),
    ('FIN','Finland','FI'),
    ('FRA','France','FR'),
    ('GUF','French Guiana','GF'),
    ('PYF','French Polynesia','PF'),
    ('ATF','French Southern Territories','TF'),
    ('GAB','Gabon','GA'),
    ('GMB','Gambia','GM'),
    ('GEO','Georgia','GE'),
    ('DEU','Germany','DE'),
    ('GHA','Ghana','GH'),
    ('GIB','Gibraltar','GI'),
    ('GRC','Greece','GR'),
    ('GRL','Greenland','GL'),
    ('GRD','Grenada','GD'),
    ('GLP','Guadeloupe','GP'),
    ('GUM','Guam','GU'),
    ('GTM','Guatemala','GT'),
    ('GGY','Guernsey','GG'),
    ('GIN','Guinea','GN'),
    ('GNB','Guinea-Bissau','GW'),
    ('GUY','Guyana','GY'),
    ('HTI','Haiti','HT'),
    ('HMD','Heard Island and McDonald Islands','HM'),
    ('HND','Honduras','HN'),
    ('HKG','Hong Kong','HK'),
    ('HUN','Hungary','HU'),
    ('ISL','Iceland','IS'),
    ('IND','India','IN'),
    ('IDN','Indonesia','ID'),
    ('IRN','Iran','IR'),
    ('IRQ','Iraq','IQ'),
    ('IRL','Ireland','IE'),
    ('IMN','Isle of Man','IM'),
    ('ISR','Israel','IL'),
    ('ITA','Italy','IT'),
    ('CIV','Ivory Coast','CI'),
    ('JAM','Jamaica','JM'),
    ('JPN','Japan','JP'),
    ('JEY','Jersey','JE'),
    ('JOR','Jordan','JO'),
    ('KAZ','Kazakhstan','KZ'),
    ('KEN','Kenya','KE'),
    ('KIR','Kiribati','KI'),
    ('XKX','Kosovo','XK'),
    ('KWT','Kuwait','KW'),
    ('KGZ','Kyrgyzstan','KG'),
    ('LAO','Laos','LA'),
    ('LVA','Latvia','LV'),
    ('LBN','Lebanon','LB'),
    ('LSO','Lesotho','LS'),
    ('LBR','Liberia','LR'),
    ('LBY','Libya','LY'),
    ('LIE','Liechtenstein','LI'),
    ('LTU','Lithuania','LT'),
    ('LUX','Luxembourg','LU'),
    ('MAC','Macao','MO'),
    ('MKD','Macedonia','MK'),
    ('MDG','Madagascar','MG'),
    ('MWI','Malawi','MW'),
    ('MYS','Malaysia','MY'),
    ('MDV','Maldives','MV'),
    ('MLI','Mali','ML'),
    ('MLT','Malta','MT'),
    ('MHL','Marshall Islands','MH'),
    ('MTQ','Martinique','MQ'),
    ('MRT','Mauritania','MR'),
    ('MUS','Mauritius','MU'),
    ('MYT','Mayotte','YT'),
    ('MEX','Mexico','MX'),
    ('FSM','Micronesia','FM'),
    ('MDA','Moldova','MD'),
    ('MCO','Monaco','MC'),
    ('MNG','Mongolia','MN'),
    ('MNE','Montenegro','ME'),
    ('MSR','Montserrat','MS'),
    ('MAR','Morocco','MA'),
    ('MOZ','Mozambique','MZ'),
    ('MMR','Myanmar [Burma]','MM'),
    ('NAM','Namibia','NA'),
    ('NRU','Nauru','NR'),
    ('NPL','Nepal','NP'),
    ('NLD','Netherlands','NL'),
    ('NCL','New Caledonia','NC'),
    ('NZL','New Zealand','NZ'),
    ('NIC','Nicaragua','NI'),
    ('NER','Niger','NE'),
    ('NGA','Nigeria','NG'),
    ('NIU','Niue','NU'),
    ('NFK','Norfolk Island','NF'),
    ('PRK','North Korea','KP'),
    ('MNP','Northern Mariana Islands','MP'),
    ('NOR','Norway','NO'),
    ('OMN','Oman','OM'),
    ('PAK','Pakistan','PK'),
    ('PLW','Palau','PW'),
    ('PSE','Palestine','PS'),
    ('PAN','Panama','PA'),
    ('PNG','Papua New Guinea','PG'),
    ('PRY','Paraguay','PY'),
    ('PER','Peru','PE'),
    ('PHL','Philippines','PH'),
    ('PCN','Pitcairn Islands','PN'),
    ('POL','Poland','PL'),
    ('PRT','Portugal','PT'),
    ('PRI','Puerto Rico','PR'),
    ('QAT','Qatar','QA'),
    ('COG','Republic of the Congo','CG'),
    ('REU','Réunion','RE'),
    ('ROU','Romania','RO'),
    ('RUS','Russia','RU'),
    ('RWA','Rwanda','RW'),
    ('BLM','Saint Barthélemy','BL'),
    ('SHN','Saint Helena','SH'),
    ('KNA','Saint Kitts and Nevis','KN'),
    ('LCA','Saint Lucia','LC'),
    ('MAF','Saint Martin','MF'),
    ('SPM','Saint Pierre and Miquelon','PM'),
    ('VCT','Saint Vincent and the Grenadines','VC'),
    ('WSM','Samoa','WS'),
    ('SMR','San Marino','SM'),
    ('STP','São Tomé and Príncipe','ST'),
    ('SAU','Saudi Arabia','SA'),
    ('SEN','Senegal','SN'),
    ('SRB','Serbia','RS'),
    ('SYC','Seychelles','SC'),
    ('SLE','Sierra Leone','SL'),
    ('SGP','Singapore','SG'),
    ('SXM','Sint Maarten','SX'),
    ('SVK','Slovakia','SK'),
    ('SVN','Slovenia','SI'),
    ('SLB','Solomon Islands','SB'),
    ('SOM','Somalia','SO'),
    ('ZAF','South Africa','ZA'),
    ('SGS','South Georgia and the South Sandwich Islands','GS'),
    ('KOR','South Korea','KR'),
    ('SSD','South Sudan','SS'),
    ('ESP','Spain','ES'),
    ('LKA','Sri Lanka','LK'),
    ('SDN','Sudan','SD'),
    ('SUR','Suriname','SR'),
    ('SJM','Svalbard and Jan Mayen','SJ'),
    ('SWZ','Swaziland','SZ'),
    ('SWE','Sweden','SE'),
    ('CHE','Switzerland','CH'),
    ('SYR','Syria','SY'),
    ('TWN','Taiwan','TW'),
    ('TJK','Tajikistan','TJ'),
    ('TZA','Tanzania','TZ'),
    ('THA','Thailand','TH'),
    ('TGO','Togo','TG'),
    ('TKL','Tokelau','TK'),
    ('TON','Tonga','TO'),
    ('TTO','Trinidad and Tobago','TT'),
    ('TUN','Tunisia','TN'),
    ('TUR','Turkey','TR'),
    ('TKM','Turkmenistan','TM'),
    ('TCA','Turks and Caicos Islands','TC'),
    ('TUV','Tuvalu','TV'),
    ('UMI','U.S. Minor Outlying Islands','UM'),
    ('VIR','U.S. Virgin Islands','VI'),
    ('UGA','Uganda','UG'),
    ('UKR','Ukraine','UA'),
    ('ARE','United Arab Emirates','AE'),
    ('GBR','United Kingdom','GB'),
    ('USA','United States','US'),
    ('URY','Uruguay','UY'),
    ('UZB','Uzbekistan','UZ'),
    ('VUT','Vanuatu','VU'),
    ('VAT','Vatican City','VA'),
    ('VEN','Venezuela','VE'),
    ('VNM','Vietnam','VN'),
    ('WLF','Wallis and Futuna','WF'),
    ('ESH','Western Sahara','EH'),
    ('YEM','Yemen','YE'),
    ('ZMB','Zambia','ZM'),
    ('ZWE','Zimbabwe','ZW');

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

INSERT INTO ships (name, company_id, country_id, ship_type_id, length, beam, draft, imo, year_built)
values ('PRELUDE', 1, 50, 1, 366, 61, 15, 9930064, 2012),
       ('PIONEERING SPIRIT', 1, 50, 1, 387, 52, 17, 9930062, 2015),
       ('CORAL-SUL FLNG', 1, 50, 1, 345, 39, 14, 9930013, 2008);

INSERT INTO cargoes (cargo_type_id)
values (1),
       (2),
       (3),
       (4),
       (5),
       (6),
       (7),
       (8);

INSERT INTO container_ships (ship_id, size_id, capacity)
VALUES (1, 2, 14501),
       (2, 2, 13722),
       (3, 2, 15731);
