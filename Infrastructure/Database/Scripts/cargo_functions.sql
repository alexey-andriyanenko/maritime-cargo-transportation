CREATE VIEW cargoes_default_view AS
SELECT cargoes.id cargo_id,
       cargo_types.id cargo_type_id,
       cargo_types.name cargo_type_name
FROM cargoes
         JOIN cargo_types ON cargoes.cargo_type_id = cargo_types.id;


CREATE FUNCTION get_cargoes_list()
RETURNS SETOF cargoes_default_view
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT * FROM cargoes_default_view;
END; $$;

CREATE FUNCTION get_cargo_by_id(p_id INT)
RETURNS SETOF cargoes_default_view
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT * FROM cargoes_default_view
    WHERE cargoes.id = p_id;
END; $$;

CREATE FUNCTION update_cargo(p_id INT, p_cargo_type_id INT)
RETURNS VOID
LANGUAGE plpgsql
AS $$
BEGIN 
    UPDATE cargoes
    SET cargo_type_id = p_cargo_type_id
    WHERE cargoes.id = p_id;
END; $$;

CREATE FUNCTION create_cargo(p_cargo_type_id INT)
RETURNS TABLE (id INT)
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    INSERT INTO cargoes (cargo_type_id)
    VALUES (p_cargo_type_id)
    RETURNING id;
END; $$;
