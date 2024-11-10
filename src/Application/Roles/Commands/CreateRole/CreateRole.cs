using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Roles.Commands.CreateRole;
[Authorize(Policy = "roles:createrole")]
public record CreateRoleCommand : IRequest<int>
{
    public required string NameEN { get; init; }
    public required string NameAR { get; init; }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _appRoleService;

    public CreateRoleCommandHandler(IApplicationDbContext context, IApplicationRoleService appRoleService)
    {
        _context = context;
        _appRoleService = appRoleService;
    }

    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new ApplicationRole
        {
            NormalizedRoleName = request.NameEN.Normalize(),
            NameEN = request.NameEN,
            NameAR = request.NameAR,
        };

        var role = await _appRoleService.CreateAsync(entity, cancellationToken);

        return role.RoleId;
    }
}
