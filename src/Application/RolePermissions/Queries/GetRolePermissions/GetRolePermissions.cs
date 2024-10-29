using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Permissions.Queries;

namespace SyriacSources.Backend.Application.RolePermissions.Queries.GetRolePermissions;

public record GetRolePermissionsQuery : IRequest<List<RolePermissionDto>>
{
    public int RoleId { get; init; }
}

public class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, List<RolePermissionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRolePermissionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<RolePermissionDto>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.ApplicationRolePermissions
            .Where(x => x.ApplicationRoleId == request.RoleId)
            .ProjectTo<RolePermissionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
