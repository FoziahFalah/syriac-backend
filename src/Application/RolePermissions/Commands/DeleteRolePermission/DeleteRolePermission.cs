
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.RolePermissions.Commands.DeleteRolePermission;

public record DeleteRolePermissionCommand :IRequest
{
    public required int Id { get; init; }
}
public class DeleteRolePermissionHandler : IRequestHandler<DeleteRolePermissionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityRoleService _identityRoleService;

    public DeleteRolePermissionHandler(IApplicationDbContext context, IIdentityRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task Handle(DeleteRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.RolePermissions
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.RolePermissions.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

    }
}
