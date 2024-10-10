using SyriacSources.Backend.Application.Common.Models;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IIdentityRoleService
{
    Task<string?> GetRoleAsync(string roleId);

    Task<List<string?>> GetRolesAsync();

    Task<(Result Result, string roleId)> CreateRoleAsync(string name, string description);

    Task<Result> UpdateRoleAsync(string roleId, string name, string description);

    Task<Result> DeleteRoleAsync(string roleId);
}
