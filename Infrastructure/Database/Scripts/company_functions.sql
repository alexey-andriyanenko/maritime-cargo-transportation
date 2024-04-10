CREATE FUNCTION get_companies_list(p_user_id INT)
RETURNS TABLE (
    company_id INT,
    company_name VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT companies.id company_id,
           companies.name company_name
    FROM companies
        JOIN users_to_companies ON users_to_companies.user_id = p_user_id
                                       AND users_to_companies.company_id = companies.id;
END; $$;

CREATE FUNCTION get_company_by_id(p_user_id INT, p_company_id INT)
RETURNS TABLE (
    company_id INT,
    company_name VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT companies.id company_id, 
           companies.name company_name
    FROM companies
        JOIN users_to_companies ON users_to_companies.user_id = p_user_id
                                       AND users_to_companies.company_id = p_company_id
    WHERE companies.id = p_company_id;
END; $$;

CREATE FUNCTION update_company(p_id INT, p_name VARCHAR)
RETURNS VOID
LANGUAGE plpgsql
AS $$
BEGIN 
    UPDATE companies
    SET name = p_name
    WHERE companies.id = p_id;
END; $$;

CREATE FUNCTION create_company(p_name VARCHAR)
RETURNS TABLE (id INT)
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    INSERT INTO companies (name) 
    VALUES (p_name) RETURNING id;
END; $$;
