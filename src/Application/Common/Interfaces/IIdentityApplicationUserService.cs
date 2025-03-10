using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.User;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IIdentityApplicationUserService
{
    Task<string?> GetUserByUsernameAsync(string userId);
    Task<string?> GetUserByEmailAsync(string userId);
    Task<bool> EmailExists(string email);
    Task<bool> AuthorizeAsync(string userId, string policyName);
    Task<(Result result, int id)> AuthenticateAsync(string email, string password);
    Task<int> CreateUserLoginAsync(ApplicationUser applicationUser, string password);
    Task<Result> DeleteUserAsync(string userId);

}
