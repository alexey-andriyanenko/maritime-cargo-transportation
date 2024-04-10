CREATE VIEW ships_default_view AS
SELECT ships.id ship_id,
       ships.name ship_name,
       countries.id country_id,
       countries.name country_name,
       countries.country_code country_country_code,
       countries.code country_code,
       ship_types.id ship_type_id,
       ship_types.name ship_type_name,
       companies.id company_id,
       companies.name company_name
FROM ships
         JOIN countries ON ships.country_id = countries.id
         JOIN ship_types ON ships.ship_type_id = ship_types.id
         JOIN companies ON ships.company_id = companies.id;

CREATE FUNCTION get_ships_list(p_company_id INT)
RETURNS SETOF ships_default_view
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT * FROM ships_default_view
    WHERE ships_default_view.company_id = p_company_id;
END; $$;

CREATE FUNCTION get_ship_by_id(p_id INT, p_company_id INT)
RETURNS SETOF ships_default_view
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT * FROM ships_default_view
    WHERE ships_default_view.ship_id = p_id
        AND ships_default_view.company_id = p_company_id;
END; $$;

CREATE FUNCTION update_ship(p_name VARCHAR, p_country_id INT, p_id INT)
RETURNS VOID
LANGUAGE plpgsql
AS $$
BEGIN 
    UPDATE ships 
    SET name = p_name,
        country_id = p_country_id
    WHERE ships.id = p_id;
END; $$;

CREATE FUNCTION create_ship(p_name VARCHAR, p_country_id INT, p_ship_type_id INT, p_company_id INT)
RETURNS TABLE( id INT )
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    INSERT INTO ships (name, country_id, ship_type_id, company_id)
    VALUES (p_name, p_country_id, p_ship_type_id, p_company_id)
    RETURNING id;
END; $$;
