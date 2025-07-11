﻿using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Permissions.Queries;

namespace SyriacSources.Backend.Application.RolePermissions.Queries.GetRolePermissions;

public record GetRolePermissionsQuery : IRequest<RolePermissionVm>
{
    public int RoleId { get; init; }
}

public class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, RolePermissionVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRolePermissionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RolePermissionVm> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ApplicationRolePermissions.Where(x => x.ApplicationRoleId == request.roleId).ToListAsync(cancellationToken);

        //Guard.Against.NotFound(request.roleId, entity);

        if (entity == null || entity.Count == 0) { return new List<int>(); }

        return entity.Select(x => x.ApplicationPermissionId).ToList();
    }
}
