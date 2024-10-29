using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Permissions.Queries.GetPermissions;

public record GetPermissionsQuery : IRequest<List<PermissionDto>> { }

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<PermissionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPermissionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .Where(x=>x.IsActive)
            .OrderBy(x => x.PolicyName)
            .ProjectTo<PermissionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
