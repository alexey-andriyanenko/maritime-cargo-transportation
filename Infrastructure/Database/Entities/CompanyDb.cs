namespace Infrastructure.Database.Entities;

public record CompanyDb
{
    public int company_id { get; init; }
    public string company_name { get; init; }
}
