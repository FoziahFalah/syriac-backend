using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.Roles;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IIdentityRoleService
{
    Task<string?> GetRoleAsync(string roleId);

    Task<List<string?>> GetRolesAsync();

    Task<(Result Result, string roleId)> CreateRoleAsync(ApplicationRoleDto role, CancellationToken cancellationToken);

    Task<Result> UpdateRoleAsync(ApplicationRoleDto role);

    Task<Result> DeleteRoleAsync(string roleId);
}
