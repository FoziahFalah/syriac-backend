using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Roles;
using SyriacSources.Backend.Domain.Constants;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Domain.Events;

namespace SyriacSources.Backend.Application.TodoItems.Commands.CreateRole;

public record CreateRoleCommand : IRequest<int>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityRoleService _identityRoleService;

    public CreateRoleCommandHandler(IApplicationDbContext context, IIdentityRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {

        var role = await _identityRoleService.CreateRoleAsync(request.Name , request.Description);
        return role.Id;
    }
}
