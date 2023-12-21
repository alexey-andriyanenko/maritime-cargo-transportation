namespace Infrastructure.Database;

public static class DatabaseHelpers
{
    public static string GetConnectionString()
    {
        return "Server=localhost;Port=5432;Database=maritime_cargo_transportation_db;User ID=postgres;Password=root;";
    }
}