using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand : IRequest<string>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
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
        var role = await _identityRoleService.CreateRoleAsync(request.Name , request.Description);

        return role.roleId;
    }
}
