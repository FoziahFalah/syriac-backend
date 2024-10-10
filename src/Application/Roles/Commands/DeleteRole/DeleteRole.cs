
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Roles.Commands.DeleteRole;

public record DeleteRoleCommand :IRequest
{
    public required int Id { get; init; }
}
public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityRoleService _identityRoleService;

    public DeleteRoleHandler(IApplicationDbContext context, IIdentityRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {

        var entity = await _identityRoleService.GetRoleAsync(request.Id.ToString());

        Guard.Against.NotFound(request.Id, entity);

        await _identityRoleService.DeleteRoleAsync(entity);

    }
}
