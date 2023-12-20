namespace Infrastructure.Database;

public static class DatabaseHelpers
{
    public static string GetConnectionString()
    {
        return "Server=postgres-container;Port=5432;Database=maritimecargotransportationdb;User ID=postgres;Password=root;";
    }
}