using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand : IRequest<string>
{
    public required string Name { get; init; }
    public required string Name_ar { get; init; }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityRoleService _identityRoleService;

    public CreateRoleCommandHandler(IApplicationDbContext context, IIdentityRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new ApplicationRoleDto
        {
            Name = request.Name,
            Name_ar = request.Name_ar,
        };
        var role = await _identityRoleService.CreateRoleAsync(entity, cancellationToken);

        return role.roleId;
    }
}
