
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Permissions.Commands.UpdatePermission;

public record UpdatePermissionCommand :IRequest<int>
{
    public int Id { get; set; }
    public required string NameAR { get; init; }
    public required string NameEN { get; init; }
    public string? Description { get; init; }
}
public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdatePermissionHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ApplicationPermissions.FindAsync(request.Id);

        Guard.Against.NotFound(request.Id, entity);

        entity.NameAR = request.NameAR;
        entity.NameEN = request.NameEN;
        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
