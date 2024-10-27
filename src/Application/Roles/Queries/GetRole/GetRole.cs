
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Roles.Queries.GetRole;

public record GetRoleQuery(int id) : IRequest<ApplicationRoleDto>
{
   
}
public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, ApplicationRoleDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _identityRoleService;
    private readonly IMapper _mapper;

    public GetRoleQueryHandler(IApplicationDbContext context, IApplicationRoleService identityRoleService, IMapper mapper)
    {
        _context = context;
        _identityRoleService = identityRoleService;
        _mapper = mapper;
    }

    public async Task<ApplicationRoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var entity = await _identityRoleService.GetRoleAsync(request.id, cancellationToken);

        Guard.Against.NotFound(request.id, entity);

        return _mapper.Map<ApplicationRoleDto>(entity);
    }
}
