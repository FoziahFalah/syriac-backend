
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.ApplicationUsers.Commands.DeleteApplicationUser;


//[Authorize(Policy = "users:deleteuser")]
public record DeleteApplicationUserCommand(int Id) : IRequest<Result> { }
public class DeleteApplicationUserHandler : IRequestHandler<DeleteApplicationUserCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityApplicationUserService _identityService;
    private readonly IApplicationUserService _appApplicationUserService;

    public DeleteApplicationUserHandler(IApplicationDbContext context, IApplicationUserService appApplicationUserService, IIdentityApplicationUserService identityService)
    {
        _context = context;
        _identityService = identityService;
        _appApplicationUserService = appApplicationUserService;
    }

    public async Task<Result> Handle(DeleteApplicationUserCommand request,CancellationToken cancellationToken)
    {
        var entity = await _identityService.DeleteUserAsync(request.Id.ToString());

        if (!entity.Succeeded)
        {
            return entity;
        }

        var contributor = await _appApplicationUserService.GetApplicationUserByIdAsync(request.Id, cancellationToken);

        if(contributor == null)
        {
            return Result.Success(null);
        }

        Guard.Against.NotFound(request.Id, entity);

        contributor.IsActive = false;

        return await _appApplicationUserService.UpdateApplicationUserAsync(contributor, cancellationToken);

    }
}
