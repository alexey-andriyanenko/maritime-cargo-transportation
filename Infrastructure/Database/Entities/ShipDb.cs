namespace Infrastructure.Database.Entities;

public record ShipDb
{
    public int ship_id { get; init; }
    public string ship_name { get; init; }
    public int country_id { get; init; }
    public string country_name { get; init; }
    public string country_country_code { get; init; }
    public string country_code { get; init; }
    public int ship_type_id { get; init; }
    public string ship_type_name { get; init; }
    public int company_id { get; init; }
    public string company_name { get; init; }
}
