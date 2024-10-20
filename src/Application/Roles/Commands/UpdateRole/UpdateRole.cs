
using SyriacSources.Backend.Application.Common.Extensions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Roles.Commands.UpdateRole;

public record UpdateRoleCommand :IRequest<Result>
{
    public int Id { get; set; }
    public required string NameEN { get; init; }
    public required string NameAR { get; init; }
}
public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _identityRoleService;

    public UpdateRoleHandler(IApplicationDbContext context, IApplicationRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _identityRoleService.GetRoleAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        ApplicationRole role = new ApplicationRole
        {
            NameEN = request.NameEN,
            NameAR = request.NameAR,
            NormalizedRoleName = request.NameEN.NormalizeString(),
            Id  = request.Id
        };

        Result result = await _identityRoleService.UpdateAsync(role,cancellationToken);

        return result;
    }
}
