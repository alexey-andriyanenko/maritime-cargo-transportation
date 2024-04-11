namespace Web.Companies.DTO;

public record CompanyResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public string Address { get; init; }
    public string Website { get; init; }
}
