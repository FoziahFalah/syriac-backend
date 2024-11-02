using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.Common;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Infrastructure.Data;
using SyriacSources.Backend.Application.Common.Extensions;
using Azure.Core;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationRoleService : IApplicationRoleService
{

    private readonly ApplicationDbContext _context;

    public ApplicationRoleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(Result , int)> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        role.NormalizedRoleName = role.NormalizedRoleName.NormalizeString();
        _context.ApplicationRoles.Add(role);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if(result > 0)
        {
            return (Result.Success(), role.Id);
        }

        return (Result.Failure(new List<string> { "Failed" }), role.Id);
    }

    public async Task<ApplicationRole?> FindByIdAsync(int roleId, CancellationToken cancellationToken)
    {
        return await _context.ApplicationRoles.FindAsync(roleId);
    }

    public async Task<ApplicationRole?> FindByNameAsync(string roleName, CancellationToken cancellationToken)
    {
        return await _context.ApplicationRoles.FirstOrDefaultAsync(r => r.NormalizedRoleName == roleName, cancellationToken);
    }
    public async Task<List<ApplicationRole>> GetRolesAsync(CancellationToken cancellationToken)
    {
        return await _context.ApplicationRoles.ToListAsync();
    }
    public async Task<ApplicationRole?> GetRoleAsync(int roleId, CancellationToken cancellationToken)
    {
        return await _context.ApplicationRoles.FindAsync(roleId);
    }

    public async Task<Result> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        role.NormalizedRoleName = role.NormalizedRoleName.NormalizeString();
        _context.ApplicationRoles.Update(role);
        return await _context.SaveChangesAsync(cancellationToken).ContinueWith(x => Result.Success());
    }

    public async Task<Result> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
    {

        _context.ApplicationRoles.Remove(role);
        return await _context.SaveChangesAsync(cancellationToken).ContinueWith(x=> Result.Success());
    }

    public async Task<Result> UpdateRolePermissions(int roleId, string permissionIds, CancellationToken cancellationToken)
    {
        // Fetch existing role permissions for the specified role
        List<ApplicationRolePermission> entity = await _context.ApplicationRolePermissions
           .Where(r => r.Id == roleId).ToListAsync();

        _context.ApplicationRolePermissions.Add(new ApplicationRolePermission
        {
            ApplicationRoleId = roleId,
            ApplicationPermissionIds = permissionIds
        });

        var result = await _context.SaveChangesAsync(cancellationToken).ContinueWith(x => Result.Success());

        return result;
    }
}
