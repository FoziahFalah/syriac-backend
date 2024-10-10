using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SyriacSources.Backend.Infrastructure.Identity;

public class IdentityRoleService : IIdentityRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly CurrentUser _user;

    public IdentityRoleService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<User> userManager,
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

    //public async Task<List<string?>> GetRoleNamesAsync()
    //{
    //    var roles = await _roleManager.Roles.Select(r=>r.Name).ToListAsync();

    //    return roles;
    //}

    public async Task<(Result Result, string roleId)> CreateRoleAsync(string name, string description)
    {
        var role = new ApplicationRole
        {
            Name = name,
            Description = description,
            CreatedOn= DateTime.UtcNow,
            CreatedBy = _user.Id, // Double check if it works
        };

        var result = await _roleManager.CreateAsync(role);

        return (result.ToApplicationResult(), role.Id.ToString());
    }
    public async Task<Result> UpdateRoleAsync(string roleId, string name, string description)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

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

        role.Name = name;
        role.NormalizedName = name;
        role.Description = description;
        role.ModifiedOn = DateTime.UtcNow;
        role.ModifiedBy = _user.Id;

        result = await _roleManager.UpdateAsync(role);

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
