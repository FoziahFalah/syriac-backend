using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Roles.Queries.GetRoles;
public record GetRolesQuery() : IRequest<List<ApplicationRoleDto>>
{

}
public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<ApplicationRoleDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _identityRoleService;
    private readonly IMapper _mapper;

    public GetRolesQueryHandler(IApplicationDbContext context, IApplicationRoleService identityRoleService, IMapper mapper)
    {
        _context = context;
        _identityRoleService = identityRoleService;
        _mapper = mapper;
    }

    public async Task<List<ApplicationRoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var entity = await _identityRoleService.GetRolesAsync(cancellationToken);

        return _mapper.Map<List<ApplicationRoleDto>>(entity);
    }
}
