
using SyriacSources.Backend.Application.Common.Extensions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.ApplicationUsers.Commands.UpdateApplicationUser;


//[Authorize(Policy = "users:updateuser")]
public record UpdateApplicationUserCommand :IRequest<Result>
{
    public int Id { get; set; }
    public required string NameEN { get; init; }
    public required string NameAR { get; init; }
}
public class UpdateApplicationUserHandler : IRequestHandler<UpdateApplicationUserCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationUserService _contributorService;

    public UpdateApplicationUserHandler(IApplicationDbContext context, IApplicationUserService contributorService)
    {
        _context = context;
        _contributorService = contributorService;
    }

    public async Task<Result> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken) { 
        var contributor = await _contributorService.GetApplicationUserByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, contributor);

        contributor.FullNameEN = request.NameEN;
        contributor.FullNameAR = request.NameAR;

        Result result = await _contributorService.UpdateApplicationUserAsync(contributor,cancellationToken);

        return result;
    }
}
