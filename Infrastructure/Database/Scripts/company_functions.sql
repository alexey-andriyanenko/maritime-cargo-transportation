CREATE FUNCTION get_companies_list(p_user_id INT)
RETURNS TABLE (
    company_id INT,
    company_name VARCHAR,
    company_address VARCHAR,
    company_phone VARCHAR,
    company_email VARCHAR,
    company_website VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT 
        companies.id company_id,
        companies.name company_name,
        companies.address company_address,
        companies.phone company_phone,
        companies.email company_email,
        companies.website company_website
    FROM companies 
        JOIN users_to_companies ON users_to_companies.user_id = p_user_id
                                       AND users_to_companies.company_id = companies.id;
END; $$;

CREATE FUNCTION get_company_by_id(p_user_id INT, p_company_id INT)
RETURNS TABLE (
    company_id INT,
    company_name VARCHAR,
    company_address VARCHAR,
    company_phone VARCHAR,
    company_email VARCHAR,
    company_website VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    SELECT
        companies.id AS company_id,
        companies.name AS company_name,
        companies.address AS company_address,
        companies.phone AS company_phone,
        companies.email AS company_email,
        companies.website AS company_website
    FROM companies
        JOIN users_to_companies ON users_to_companies.user_id = p_user_id
                                       AND users_to_companies.company_id = p_company_id
    WHERE companies.id = p_company_id;
END; $$;

CREATE FUNCTION update_company(p_id INT, p_name VARCHAR, p_address VARCHAR, p_phone VARCHAR, p_email VARCHAR, p_website VARCHAR)
RETURNS VOID
LANGUAGE plpgsql
AS $$
BEGIN 
    UPDATE companies
    SET name = p_name,
        address = p_address,
        phone = p_phone,
        email = p_email,
        website = p_website
    WHERE companies.id = p_id;
END; $$;

CREATE FUNCTION create_company(p_name VARCHAR, p_address VARCHAR, p_phone VARCHAR, p_email VARCHAR, p_website VARCHAR)
RETURNS TABLE (id INT)
LANGUAGE plpgsql
AS $$
BEGIN 
    RETURN QUERY
    INSERT INTO companies (name, address, phone, email, website)
    VALUES (p_name, p_address, p_phone, p_email, p_website) RETURNING id;
END; $$;
