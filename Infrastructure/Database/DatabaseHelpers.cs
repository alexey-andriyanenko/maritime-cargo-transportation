namespace Infrastructure.Database;

public static class DatabaseHelpers
{
    public static string GetConnectionString()
    {
        return "Server=maritime-db;Port=5432;Database=maritime_cargo_transportation_db;User ID=postgres;Password=root;";
    }
}