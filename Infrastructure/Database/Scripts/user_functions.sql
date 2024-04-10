CREATE VIEW users_default_view AS
SELECT users.id user_id,
       users.first_name user_first_name,
       users.last_name user_last_name,
       users.email user_email,
       users.password user_password,
       companies.id company_id,
       companies.name company_name
FROM users
         JOIN users_to_companies ON users.id = users_to_companies.user_id
         JOIN companies ON users_to_companies.company_id = companies.id;

CREATE FUNCTION get_users_list()
RETURNS SETOF users_default_view
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT * FROM users_default_view;
END; $$;

CREATE FUNCTION get_user_by_id(p_id INT)
RETURNS SETOF users_default_view
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY 
    SELECT * FROM users_default_view
    WHERE users_default_view.user_id = p_id;
END; $$;

CREATE FUNCTION get_user_by_email(p_email VARCHAR)
RETURNS SETOF users_default_view
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT * FROM users_default_view
    WHERE users_default_view.user_email = p_email;
END; $$;

CREATE FUNCTION update_user( p_id INT, p_first_name VARCHAR, p_last_name VARCHAR, p_email VARCHAR)
RETURNS VOID
LANGUAGE plpgsql
AS $$
BEGIN 
    UPDATE users
    SET first_name = p_first_name, 
        last_name = p_last_name,
        email = p_email 
    WHERE users.id = p_id;
END; $$;

CREATE FUNCTION create_user(p_first_name VARCHAR, p_last_name VARCHAR, p_email VARCHAR, p_password VARCHAR)
RETURNS TABLE (id INT)
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    INSERT INTO users (email, password, first_name, last_name)
    VALUES (p_email, p_password, p_first_name, p_last_name)
    RETURNING id;
END; $$;
