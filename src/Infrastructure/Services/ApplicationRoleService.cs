
using SyriacSources.Backend.Application.Common.Models;
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

    public async Task<Result> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        role.NormalizedRoleName = role.NormalizedRoleName.NormalizeString();
        _context.ApplicationRoles.Add(role);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if(result > 0)
        {
            return (Result.Success(role.Id));
        }

        return (Result.Failure(new List<string> { "Failed" }));
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

    public async Task<List<ApplicationRole>?> GetRolesAsync(List<int> roleIds, CancellationToken cancellationToken)
    {
        
        return await _context.ApplicationRoles.Where(x=> roleIds.Any(r => r == x.Id)).ToListAsync();
    }

    public async Task<Result> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        role.NormalizedRoleName = role.NormalizedRoleName.NormalizeString();
        _context.ApplicationRoles.Update(role);
        return await _context.SaveChangesAsync(cancellationToken).ContinueWith(x => Result.Success(role.Id));
    }

    public async Task<Result> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
    {

        _context.ApplicationRoles.Remove(role);
        return await _context.SaveChangesAsync(cancellationToken).ContinueWith(x=> Result.Success(null));
    }

    public async Task<Result> UpdateRolePermissions(int roleId, string permissionIds, CancellationToken cancellationToken)
    {

        // Convert comma-separated permissionIds into a list of integers
        var permissionIdList = permissionIds.Split(',')
                                            .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                                            .Where(id => id.HasValue)
                                            .Select(id => id!.Value)
                                            .ToList();

        if (!permissionIdList.Any())
        {
            return Result.Failure("Invalid permissions provided.");
        }

        // Fetch existing role permissions for the specified role
        var existingPermissions = await _context.ApplicationRolePermissions
            .Where(rp => rp.ApplicationRoleId == roleId)
            .ToListAsync(cancellationToken);

        // Remove old permissions
        _context.ApplicationRolePermissions.RemoveRange(existingPermissions);

        // Add new permissions
        var newPermissions = permissionIdList.Select(permissionId => new ApplicationRolePermission
        {
            ApplicationRoleId = roleId,
            ApplicationPermissionId = permissionId
        });

        await _context.ApplicationRolePermissions.AddRangeAsync(newPermissions, cancellationToken);

        // Save changes
        var result = await _context.SaveChangesAsync(cancellationToken).ContinueWith(x => Result.Success(null));

        return result;
    }
}
