﻿namespace Infrastructure.Security.Services;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUsername() => 
        _httpContextAccessor.HttpContext!.User.FindFirstValue("name")!;
}