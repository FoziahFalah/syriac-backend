using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.ApplicationUsers.Queries.GetApplicationUser;


//[Authorize(Policy = "users:getuser")]
public record GetApplicationUsersQuery(int id) : IRequest<ApplicationUser>
{
   
}
public class GetApplicationUsersQueryHandler : IRequestHandler<GetApplicationUsersQuery, ApplicationUser>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationUserService _applicationUserService;
    private readonly IMapper _mapper;

    public GetApplicationUsersQueryHandler(IApplicationDbContext context, IApplicationUserService applicationUserService, IMapper mapper)
    {
        _context = context;
        _applicationUserService = applicationUserService;
        _mapper = mapper;
    }

    public async Task<ApplicationUser> Handle(GetApplicationUsersQuery request, CancellationToken cancellationToken)
    {
        var entity = await _applicationUserService.GetApplicationUserByIdAsync(request.id, cancellationToken);

        Guard.Against.NotFound(request.id, entity);

        return entity;
    }
}
