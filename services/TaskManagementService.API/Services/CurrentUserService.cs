using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskManagementService.Application.Interfaces;

namespace TaskManagementService.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return string.IsNullOrEmpty(userIdString) ? Guid.Empty : Guid.Parse(userIdString);
        }
    }
}