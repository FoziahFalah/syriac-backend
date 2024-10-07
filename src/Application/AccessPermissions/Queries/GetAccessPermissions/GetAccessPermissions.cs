using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Mappings;
using SyriacSources.Backend.Application.Common.Models;

namespace SyriacSources.Backend.Application.TodoItems.Queries.GetAccessPermissions;

public record GetAccessPermissionsQuery : IRequest<List<AccessPermissionsDto>>
{
    public int RoleId { get; init; }
}

public class GetAccessPermissionsQueryHandler : IRequestHandler<GetAccessPermissionsQuery, List<AccessPermissionsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAccessPermissionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AccessPermissionsDto>> Handle(GetAccessPermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.TodoItems
            .Where(x => x.ListId == request.RoleId)
            .OrderBy(x => x.Title)
            .ProjectTo<AccessPermissionsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
