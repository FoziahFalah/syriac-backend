using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.Roles;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IApplicationRoleService
{
    Task<ApplicationRole?> FindByIdAsync(int roleId, CancellationToken cancellationToken);
    Task<ApplicationRole?> FindByNameAsync(string roleName, CancellationToken cancellationToken);
    //Task<List<int>> GetAsync(CancellationToken cancellationToken);
    Task<(Result Result, int roleId)> CreateAsync(ApplicationRole role, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken);
}
