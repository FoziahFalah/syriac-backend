using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand : IRequest<string>
{
    public required string Name { get; init; }
    public required string NameAR { get; init; }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _identityRoleService;

    public CreateRoleCommandHandler(IApplicationDbContext context, IApplicationRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new ApplicationRoleDto
        {
            Name = request.Name,
            NameAR = request.NameAR,
        };
        var role = await _identityRoleService.CreateRoleAsync(entity, cancellationToken);

        return role.roleId;
    }
}
