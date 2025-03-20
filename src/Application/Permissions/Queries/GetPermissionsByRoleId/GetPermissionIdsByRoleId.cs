using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Permissions.Queries.GetPermissionsByRoleId;

public record GetPermissionsByRoleIdQuery(int roleId) : IRequest<List<int>>
{
}

public class GetPermissionsByRoleIdQueryHandler : IRequestHandler<GetPermissionsByRoleIdQuery, List<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPermissionsByRoleIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<int>> Handle(GetPermissionsByRoleIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ApplicationRolePermissions.Where(x => x.ApplicationRoleId == request.roleId).ToListAsync(cancellationToken);

        //Guard.Against.NotFound(request.roleId, entity);

        if( entity == null || entity.Count == 0) { return new List<int>(); }

        return entity.Select(x=>x.ApplicationPermissionId).ToList();
    }
}
