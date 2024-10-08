using SyriacSources.Backend.Application.Common.Models;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IIdentityRoleService
{
    Task<string?> GetRoleAsync(string roleId);

    Task<(Result Result, string roleId)> CreateRoleAsync(string name, string description);

    //Task<Result> AddPermissionToRole(string roleId, List<int> permissions);

    Task<Result> DeleteRoleAsync(string roleId);
}
