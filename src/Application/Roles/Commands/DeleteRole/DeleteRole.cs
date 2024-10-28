
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Roles.Commands.DeleteRole;

public record DeleteRoleCommand() : IRequest {
    public int Id { get; set; }
}
public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _appRoleService;

    public DeleteRoleHandler(IApplicationDbContext context, IApplicationRoleService appRoleService)
    {
        _context = context;
        _appRoleService = appRoleService;
    }

    public async Task Handle(DeleteRoleCommand request,CancellationToken cancellationToken)
    {

        var entity = await _appRoleService.GetRoleAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        await _appRoleService.DeleteAsync(entity, cancellationToken);

    }
}
