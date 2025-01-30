
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Contributors.Commands.DeleteContributor;


//[Authorize(Policy = "users:deleteuser")]
public record DeleteContributorCommand(int Id) : IRequest<Result> { }
public class DeleteContributorHandler : IRequestHandler<DeleteContributorCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityApplicationUserService _identityService;
    private readonly IContributorService _appContributorService;

    public DeleteContributorHandler(IApplicationDbContext context, IContributorService appContributorService, IIdentityApplicationUserService identityService)
    {
        _context = context;
        _identityService = identityService;
        _appContributorService = appContributorService;
    }

    public async Task<Result> Handle(DeleteContributorCommand request,CancellationToken cancellationToken)
    {
        var entity = await _identityService.DeleteUserAsync(request.Id.ToString());

        if (!entity.Succeeded)
        {
            return entity;
        }

        var contributor = await _appContributorService.GetContributorByIdAsync(request.Id, cancellationToken);

        if(contributor == null)
        {
            return Result.Success(null);
        }

        Guard.Against.NotFound(request.Id, entity);

        contributor.IsActive = false;

        return await _appContributorService.UpdateContributorAsync(contributor, cancellationToken);

    }
}
