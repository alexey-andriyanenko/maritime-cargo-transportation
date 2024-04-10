using Domain.Company.Entities;
using Domain.User.Entities;

namespace Infrastructure.Database.Entities;

public static class UserDbExtentions
{
    public static User ToDomain(this UserDb userDb, IGrouping<int, UserDb> groups)
    {
        return new User
        {
            Id = userDb.user_id,
            FirstName = userDb.user_first_name,
            LastName = userDb.user_last_name,
            Email = userDb.user_email,
            Password = userDb.user_password,
            Companies = groups
                .Select(u => new Company { Id = u.company_id, Name = u.company_name })
                .ToList()
        };
    }
}
