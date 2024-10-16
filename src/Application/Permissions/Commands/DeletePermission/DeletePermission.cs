
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Permissions.Commands.DeletePermission;

public record DeletePermissionCommand :IRequest
{
    public required int Id { get; init; }
}
public class DeletePermissionHandler : IRequestHandler<DeletePermissionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _identityRoleService;

    public DeletePermissionHandler(IApplicationDbContext context, IApplicationRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Permissions
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Permissions.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

    }
}
