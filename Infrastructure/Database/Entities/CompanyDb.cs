namespace Infrastructure.Database.Entities;

public record CompanyDb
{
    public int company_id { get; init; }
    public string company_name { get; init; }
    public string company_email { get; init; }
    public string company_phone { get; init; }
    public string company_address { get; init; }
    public string company_website { get; init; }
}
