
using SyriacSources.Backend.Application.Common.Extensions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Contributors.Commands.UpdateContributor;


//[Authorize(Policy = "users:updateuser")]
public record UpdateContributorCommand :IRequest<Result>
{
    public int Id { get; set; }
    public required string NameEN { get; init; }
    public required string NameAR { get; init; }
}
public class UpdateContributorHandler : IRequestHandler<UpdateContributorCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IContributorService _contributorService;

    public UpdateContributorHandler(IApplicationDbContext context, IContributorService contributorService)
    {
        _context = context;
        _contributorService = contributorService;
    }

    public async Task<Result> Handle(UpdateContributorCommand request, CancellationToken cancellationToken) { 
        var contributor = await _contributorService.GetContributorByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, contributor);

        contributor.FullNameEN = request.NameEN;
        contributor.FullNameAR = request.NameAR;

        Result result = await _contributorService.UpdateContributorAsync(contributor,cancellationToken);

        return result;
    }
}
