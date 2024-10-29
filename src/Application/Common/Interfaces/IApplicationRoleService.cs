using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.Roles;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IApplicationRoleService
{
    Task<ApplicationRole?> FindByIdAsync(int roleId, CancellationToken cancellationToken);
    Task<ApplicationRole?> FindByNameAsync(string roleName, CancellationToken cancellationToken);
    Task<List<ApplicationRole>> GetRolesAsync(CancellationToken cancellationToken);
    Task<ApplicationRole?> GetRoleAsync(int roleId, CancellationToken cancellationToken);
    Task<(Result Result, int RoleId)> CreateAsync(ApplicationRole role, CancellationToken cancellationToken);
    Task<Result> UpdateRolePermissions(int roleId, List<int> permissionIds, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken);
}
