namespace Infrastructure.Database.Entities;

public record UserDb
{
    public int user_id { get; init; }
    public string user_first_name { get; init; }
    public string user_last_name { get; init; }
    public string user_email { get; init; }
    public string user_password { get; init; }
    public int company_id { get; init; }
    public string company_name { get; init; }
}
