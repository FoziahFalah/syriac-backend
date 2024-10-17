using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand : IRequest<string>
{
    public required string NameEn { get; init; }
    public required string NameAR { get; init; }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _appRoleService;

    public CreateRoleCommandHandler(IApplicationDbContext context, IApplicationRoleService appRoleService)
    {
        _context = context;
        _appRoleService = appRoleService;
    }

    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new ApplicationRole
        {
            NormalizedRoleName = request.NameEn.Normalize(),
            NameEN = request.NameEn,
            NameAR = request.NameAR,
        };

        var role = await _appRoleService.CreateAsync(entity, cancellationToken);

        return role.Result;
    }
}
