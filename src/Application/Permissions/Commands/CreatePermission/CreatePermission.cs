
using SyriacSources.Backend.Application.Common.Extensions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Permissions.Commands.CreatePermission;

public record CreatePermissionCommand :IRequest<int>
{
    public required string Name { get; init; }
    public required string NameAR { get; init; }
    public required string NameEN { get; init; }
    public required string Description { get; init; }
    public int ParentId { get; set; }
    public bool IsModule { get; set; }
}
public class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _identityRoleService;

    public CreatePermissionHandler(IApplicationDbContext context, IApplicationRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task<int> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = new ApplicationPermission
        {
            PolicyName = request.Name.NormalizeString(),
            NameAR = request.NameAR,
            NameEN = request.NameEN,
            ParentId = request.ParentId,
            IsModule = request.IsModule,
            Description = request.Description
        };

        _context.Permissions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
