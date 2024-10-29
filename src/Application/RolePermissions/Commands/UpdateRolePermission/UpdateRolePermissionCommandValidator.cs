
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.RolePermissions.Commands.UpdateRolePermission;

public record UpdateRolePermissionCommand : IRequest
{
    public int RoleId { get; set; }
    public List<int> PermissionIds { get; set; } = new List<int>();
}
public class UpdateRolePermissionHandler : IRequestHandler<UpdateRolePermissionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _appRoleService;

    public UpdateRolePermissionHandler(IApplicationDbContext context, IApplicationRoleService appRoleService)
    {
        _context = context;
        _appRoleService = appRoleService;
    }

    public async Task Handle(UpdateRolePermissionCommand request, CancellationToken cancellationToken)
    {

        var result = await _appRoleService.UpdateRolePermissions(request.RoleId, request.PermissionIds, cancellationToken);

    }
}
