using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.RolePermissions.Queries.GetRolePermissions;

public record GetPermissionsQuery : IRequest<List<RolePermissionDto>>
{
    public int RoleId { get; init; }
}

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<RolePermissionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPermissionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<RolePermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.TodoItems
            .Where(x => x.ListId == request.RoleId)
            .OrderBy(x => x.Title)
            .ProjectTo<RolePermissionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
