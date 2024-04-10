CREATE VIEW containers_default_view AS
SELECT containers.id container_id,
       containers.container_ship_id container_ship_id,
       container_types.id container_type_id,
       container_types.name container_type_name,
       cargoes.id  cargo_id,
       cargo_types.id cargo_type_id,
       cargo_types.name cargo_type_name
FROM containers
         JOIN container_types ON containers.container_type_id = container_types.id
         JOIN cargoes_to_containers ON containers.id = cargoes_to_containers.container_id
         JOIN cargoes ON  cargoes_to_containers.cargo_id = cargoes.id
         JOIN cargo_types ON cargoes.cargo_type_id = cargo_types.id;

CREATE FUNCTION get_containers_list()
RETURNS SETOF containers_default_view
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT * FROM containers_default_view;
END; $$;

CREATE FUNCTION get_container_by_id(p_id INT)
RETURNS SETOF containers_default_view
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT * FROM containers_default_view
    WHERE containers.id = p_id;
END; $$;

CREATE FUNCTION update_container(p_id INT, p_container_ship_id INT, p_cargo_id INT)
RETURNS VOID
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE containers 
    SET container_ship_id = p_container_ship_id,
        cargo_id = p_cargo_id
    WHERE containers.id = p_id;
END; $$;

CREATE FUNCTION attach_container_to_container_ship(p_container_id INT, p_container_ship_id INT)
RETURNS VOID
LANGUAGE plpgsql
AS $$
BEGIN 
    UPDATE containers 
    SET container_ship_id = p_container_ship_id
    WHERE containers.id = p_container_id;
END ; $$;

CREATE FUNCTION detach_container_from_container_ship(p_container_id INT)
RETURNS VOID
LANGUAGE plpgsql
AS $$
BEGIN 
    UPDATE containers 
    SET container_ship_id = NULL
    WHERE containers.id = p_container_id;
END; $$;
