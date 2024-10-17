using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.Users;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IApplicationUserRoleService
{
    Task<(Result,int)> AddToRolesAsync(int userId, List<int> roles, CancellationToken cancellationToken);
    Task<bool> IsInRoleAsync(int userId, int roleId, CancellationToken cancellationToken);
}
