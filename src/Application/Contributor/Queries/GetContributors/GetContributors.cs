
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Contributors.Queries.GetContributor;


//[Authorize(Policy = "users:getuser")]
public record GetContributorsQuery(int id) : IRequest<ApplicationUser>
{
   
}
public class GetContributorQueryHandler : IRequestHandler<GetContributorQuery, ApplicationUser>
{
    private readonly IApplicationDbContext _context;
    private readonly IContributorService _contributorService;
    private readonly IMapper _mapper;

    public GetContributorsQueryHandler(IApplicationDbContext context, IContributorService contributorService, IMapper mapper)
    {
        _context = context;
        _contributorService = contributorService;
        _mapper = mapper;
    }

    public async Task<ApplicationUser> Handle(GetContributorQuery request, CancellationToken cancellationToken)
    {
        var entity = await _contributorService.GetContributorByIdAsync(request.id, cancellationToken);

        Guard.Against.NotFound(request.id, entity);

        return entity;
    }
}
