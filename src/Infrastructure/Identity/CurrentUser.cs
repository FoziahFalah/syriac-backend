using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Infrastructure.Identity;
public class CurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
