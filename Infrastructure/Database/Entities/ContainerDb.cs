namespace Infrastructure.Database.Entities;

public record ContainerDb
{
    public int container_id { get; init; }
    public int container_type_id { get; init; }
    public string container_type_name { get; init; }
    public int cargo_id { get; init; }
    public int cargo_type_id { get; init; }
    public string cargo_type_name { get; init; }
    public int container_ship_id { get; init; }
}
