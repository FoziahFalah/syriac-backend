
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.ApplicationRolePermissions.Commands.UpdateRolePermission;

public record UpdateRolePermissionCommand :IRequest<int>
{
    public int RoleId { get; set; }
    public required List<int> PermissionIds { get; init; }
}

public class UpdateRolePermissionHandler : IRequestHandler<UpdateRolePermissionCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _appRoleService;

    public UpdateRolePermissionHandler(IApplicationDbContext context, IApplicationRoleService appRoleService)
    {
        _context = context;
        _appRoleService = appRoleService;
    }

    public async Task<int> Handle(UpdateRolePermissionCommand request, CancellationToken cancellationToken)
    {

        var result = await _appRoleService.UpdateRolePermissions(request.RoleId, request.PermissionIds, cancellationToken);


        // Return the number of changes made
        return result.countChanges;
    }
}
