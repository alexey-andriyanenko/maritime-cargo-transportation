namespace Infrastructure.Database.Entities;

public record CargoDb
{
    public int cargo_id { get; init; }
    public int cargo_type_id { get; init; }
    public string cargo_type_name { get; init; }
}
