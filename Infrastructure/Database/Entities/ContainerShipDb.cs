namespace Infrastructure.Database.Entities;

public record ContainerShipDb
{
    public int container_ship_id { get; init; }
    public int container_ship_capacity { get; init; }
    public int ship_id { get; init; }
    public string ship_name { get; init; }
    public int ship_length { get; init; }
    public int ship_beam { get; init; }
    public int ship_draft { get; init; }
    public int ship_year_built { get; init; }
    public int ship_imo { get; init; }
    public int country_id { get; init; }
    public string country_name { get; init; }
    public string country_country_code { get; init; }
    public string country_code { get; init; }
    public int ship_type_id { get; init; }
    public string ship_type_name { get; init; }
    public int size_type_id { get; init; }
    public string size_type_name { get; init; }
    public int container_id { get; init; }
    public int container_type_id { get; init; }
    public string container_type_name { get; init; }
    public int cargo_id { get; init; }
    public int cargo_type_id { get; init; }
    public string cargo_type_name { get; init; }
    public int company_id { get; init; }
    public string company_name { get; init; }
    public string company_email { get; init; }
    public string company_phone { get; init; }
    public string company_address { get; init; }
    public string company_website { get; init; }
}
