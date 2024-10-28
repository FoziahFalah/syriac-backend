using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.User;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> EmailExists(string email);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, int Id)> AuthenticateAsync(string email, string password);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);

}
