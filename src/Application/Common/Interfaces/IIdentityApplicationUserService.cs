using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.User;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IIdentityApplicationUserService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> EmailExists(string email);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<Result> AuthenticateAsync(string email, string password);

    Task<int> CreateUserLoginAsync(string userName, string password, int userId);

    Task<Result> DeleteUserAsync(string userId);

}
