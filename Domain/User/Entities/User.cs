using System.Collections.Generic;
using Domain.Shared.Entities;

namespace Domain.User.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public IReadOnlyCollection<Company.Entities.Company> Companies { get; set; }
}