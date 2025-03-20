using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Permissions.Queries.GetPermissions;

public record GetPermissionsQuery : IRequest<List<PermissionTreeNodeDto>> { }

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<PermissionTreeNodeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPermissionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PermissionTreeNodeDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {

        var permissions = await _context.ApplicationPermissions.AsNoTracking().ToListAsync(cancellationToken);
        return BuildPermissionTree(permissions,0);
    }

    private List<PermissionTreeNodeDto> BuildPermissionTree(List<ApplicationPermission> permissions, int parentId = 0)
    {
        return permissions
            .Where(p => p.ParentId == parentId && p.Id != parentId)
            .Select(p => new PermissionTreeNodeDto
            {
                Key = p.Id.ToString(),
                Checked = false,
                Data = new ApplicationPermissionDto
                {
                    Id = p.Id,
                    IsModule = p.IsModule,
                    NameEN = p.NameEN ?? string.Empty,
                    NameAR = p.NameAR ?? string.Empty,
                    ParentId = p.ParentId,
                    Description = p.Description
                },
                Children = BuildPermissionTree(permissions, p.Id)
            })
            .ToList();
    }
}
