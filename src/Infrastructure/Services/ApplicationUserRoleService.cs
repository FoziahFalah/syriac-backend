using Microsoft.Extensions.Options;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Infrastructure.Data;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationUserRoleService : IApplicationUserRoleService
{
    public readonly ApplicationDbContext _context;
    public readonly JsonDelimiters _jsonDelimiters;

    public ApplicationUserRoleService(ApplicationDbContext context,IOptions<JsonDelimiters> jsonDelimiters)
    {
        _context = context;
        _jsonDelimiters = jsonDelimiters.Value;
    }

    
    public async Task<Result> AddToRolesAsync(int userId, List<int> roles, CancellationToken cancellationToken)
    {
        // Fetch existing role permissions for the specified role
        var existingRoles = await _context.ApplicationUserRoles
                                        .Where(rp => rp.ApplicationUserId == userId)
                                        .ToListAsync(cancellationToken);

        // Remove old permissions
        _context.ApplicationUserRoles.RemoveRange(existingRoles);

        // Add new permissions
        var newRoles = roles.Select(roleId => new ApplicationUserRole
        {
            ApplicationRoleId = roleId,
            ApplicationUserId = userId
        });

        await _context.ApplicationUserRoles.AddRangeAsync(newRoles, cancellationToken);

        // Save changes
        return await _context.SaveChangesAsync(cancellationToken).ContinueWith(x => Result.Success(null));
    }


    public async Task<bool> IsInRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
    {
        return await _context.ApplicationUserRoles.AnyAsync(r => r.ApplicationUserId == userId && r.ApplicationRoleId == roleId);

    }
}
