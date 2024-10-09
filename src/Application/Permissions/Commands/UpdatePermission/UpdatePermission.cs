
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Permissions.Commands.UpdatePermission;

public record UpdatePermissionCommand :IRequest<int>
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}
public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityRoleService _identityRoleService;

    public UpdatePermissionHandler(IApplicationDbContext context, IIdentityRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task<int> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Permissions
           .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.PermissionName = request.Name;
        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
