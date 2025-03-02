
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.ApplicationUsers.Queries.GetApplicationUser;


//[Authorize(Policy = "users:getuser")]
public record GetApplicationUserQuery(int id) : IRequest<ApplicationUser>
{
   
}
public class GetApplicationUserQueryHandler : IRequestHandler<GetApplicationUserQuery, ApplicationUser>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationUserService _applicationUserService;
    private readonly IMapper _mapper;

    public GetApplicationUserQueryHandler(IApplicationDbContext context, IApplicationUserService applicationUserService, IMapper mapper)
    {
        _context = context;
        _applicationUserService = applicationUserService;
        _mapper = mapper;
    }

    public async Task<ApplicationUser> Handle(GetApplicationUserQuery request, CancellationToken cancellationToken)
    {
        var entity = await _applicationUserService.GetApplicationUserByIdAsync(request.id, cancellationToken);

        Guard.Against.NotFound(request.id, entity);

        return entity;
    }
}
