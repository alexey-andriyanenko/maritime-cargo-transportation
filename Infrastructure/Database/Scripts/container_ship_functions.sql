CREATE VIEW container_ships_default_view AS
SELECT container_ships.id container_ship_id,
       container_ships.capacity container_ship_capacity,
       ships.id ship_id,
       ships.name ship_name,
       ships.length ship_length,
       ships.beam ship_beam,
       ships.draft ship_draft,
       ships.imo ship_imo,
       ships.year_built ship_year_built,
       countries.id country_id,
       countries.name country_name,
       countries.country_code country_country_code,
       countries.code country_code,
       ship_types.id ship_type_id,
       ship_types.name ship_type_name,
       companies.id company_id,
       companies.name company_name,
       companies.address company_address,
       companies.phone company_phone,
       companies.email company_email,
       companies.website company_website,
       container_ship_size_types.id size_type_id,
       container_ship_size_types.name size_type_name,
       containers.id container_id,
       container_types.id container_type_id,
       container_types.name container_type_name,
       cargoes.id cargo_id,
       cargo_types.id cargo_type_id,
       cargo_types.name cargo_type_name
FROM container_ships
         JOIN ships ON container_ships.ship_id = ships.id
         JOIN ship_types ON ships.ship_type_id = ship_types.id
         JOIN countries ON ships.country_id = countries.id
         JOIN companies on ships.company_id = companies.id
         JOIN container_ship_size_types ON container_ships.size_id = container_ship_size_types.id
         LEFT JOIN containers ON containers.container_ship_id = container_ships.id
         LEFT JOIN container_types ON container_types.id = containers.container_type_id
         LEFT JOIN cargoes_to_containers ON cargoes_to_containers.container_id = containers.id
         LEFT JOIN cargoes ON cargoes_to_containers.cargo_id = cargoes.id
         LEFT JOIN cargo_types ON cargo_types.id = cargoes.cargo_type_id;

CREATE FUNCTION get_container_ships_list(p_company_id INT)
RETURNS SETOF container_ships_default_view
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT * FROM container_ships_default_view
    WHERE container_ships_default_view.company_id = p_company_id;
END; $$;

CREATE FUNCTION get_container_ship_by_id(p_id INT, p_company_id INT)
RETURNS SETOF container_ships_default_view
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT * FROM container_ships_default_view
    WHERE container_ships_default_view.ship_id = p_id
        AND container_ships_default_view.company_id = p_company_id;
END; $$;

CREATE FUNCTION create_container_ship(p_ship_id INT, p_size_id INT, p_capacity INT)
RETURNS TABLE(id INT)
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    INSERT INTO container_ships (ship_id, size_id, capacity)
    VALUES (p_ship_id, p_size_id, p_capacity)
    RETURNING id;
END; $$;
