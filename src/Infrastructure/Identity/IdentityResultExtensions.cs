using SyriacSources.Backend.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace SyriacSources.Backend.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success(null)
            : Result.Failure(result.Errors.Select(e => e.Description));
    }
}
