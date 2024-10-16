
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Roles.Commands.UpdateRole;

public record UpdateRoleCommand :IRequest<Result>
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public required string Name_ar { get; init; }
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
        var entity = await _identityRoleService.GetRoleAsync(request.Id.ToString());

        Guard.Against.NotFound(request.Id, entity);
        ApplicationRoleDto role = new ApplicationRoleDto
        {
            Name = request.Name,
            NameAR = request.Name_ar,
            Id  = request.Id
        };

        Result result = await _identityRoleService.UpdateRoleAsync(role);

        return result;
    }
}
