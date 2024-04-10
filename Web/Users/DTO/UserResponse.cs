﻿namespace Web.Users.DTO;

public record UserResponse
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
}
