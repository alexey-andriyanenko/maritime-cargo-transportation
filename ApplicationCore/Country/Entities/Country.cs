using Domain.Shared.Entities;

namespace Domain.Country.Entities;

public class Country : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string CountryCode { get; set; }
}