using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Roles;

namespace SyriacSources.Backend.Infrastructure.Identity;

public class IdentityRoleService : IIdentityRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly CurrentUser _user;

    public IdentityRoleService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        CurrentUser user,
        IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
        _roleManager = roleManager;
        _userManager = userManager;
        _user = user;
    }

    public async Task<string?> GetRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        return role?.Name;
    }

    public async Task<List<string?>> GetRolesAsync()
    {
        var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

        return roles;
    }

    public async Task<(Result Result, string roleId)> CreateRoleAsync(ApplicationRoleDto role,CancellationToken cancellationToken)
    {
        var entity = new ApplicationRole
        {
            Name = role.Name,
            Name_ar = role.Name_ar,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = _user.Id, // Double check if it works
        };

        var result = await _roleManager.CreateAsync(entity);

        return (result.ToApplicationResult(), role.Id.ToString());
    }
    public async Task<Result> UpdateRoleAsync(ApplicationRoleDto role)
    {
        var entity = await _roleManager.FindByIdAsync(role.Id.ToString());

        IdentityResult result = new IdentityResult();

        if (role == null)
        {
            result = IdentityResult.Failed(
               new IdentityError {
                   Code = "0001",
                   Description = "Not Found"
               });
            return result.ToApplicationResult();
        }

        entity!.Name = role.Name;
        entity!.Name_ar = role?.Name_ar;
        entity!.ModifiedOn = DateTime.UtcNow;
        entity!.ModifiedBy = _user.Id;

        
        result = await _roleManager.UpdateAsync(entity);

        return (result.ToApplicationResult());
    }

    public async Task<Result> DeleteRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        return role != null ? await DeleteRoleAsync(role) : Result.Success();
    }

    public async Task<Result> DeleteRoleAsync(ApplicationRole role)
    {
        role.IsActive = false;

        var result = await _roleManager.UpdateAsync(role);

        return result.ToApplicationResult();
    }
}
