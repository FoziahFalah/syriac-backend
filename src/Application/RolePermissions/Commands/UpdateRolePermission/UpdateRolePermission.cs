
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.RolePermissions.Commands.UpdateRolePermission;

public record UpdateRolePermissionCommand :IRequest<int>
{
    public Guid RoleId { get; set; }
    public required List<int> PermissionId { get; init; }
}

public class UpdateRolePermissionHandler : IRequestHandler<UpdateRolePermissionCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityRoleService _identityRoleService;

    public UpdateRolePermissionHandler(IApplicationDbContext context, IIdentityRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task<int> Handle(UpdateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        // Fetch existing role permissions for the specified role
        var entity = await _context.RolePermissions
           .Where(r => r.RoleId == request.RoleId).ToListAsync();

        var currentPermissionIds = entity.Select(r => r.Id);

        // Find permissions to add
        var permissionsToAdd = request.PermissionId.Except(currentPermissionIds).ToList();
        foreach (var permissionId in permissionsToAdd)
        {
            _context.RolePermissions.Add(new RolePermission
            {
                RoleId = request.RoleId,
                PermissionId = permissionId.ToString()
            });
        }

        // Find permissions to remove
        var permissionsToRemove = currentPermissionIds.Except(request.PermissionId).ToList();
        foreach (var permissionId in permissionsToRemove)
        {
            var permissionToRemove = entity.First(rp => rp.PermissionId == permissionId.ToString());
            _context.RolePermissions.Remove(permissionToRemove);
        }


        await _context.SaveChangesAsync(cancellationToken);

        // Return the number of changes made
        return permissionsToAdd.Count + permissionsToRemove.Count;
    }
}
