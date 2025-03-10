using System.Xml.Linq;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.ApplicationUsers.Commands.CreateApplicationUser;

public record CreateApplicationUserCommand : IRequest<Result>
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; } = "Aa@123456";
}

public class CreateApplicationUserCommandHandler : IRequestHandler<CreateApplicationUserCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityApplicationUserService _identityService;

    public CreateApplicationUserCommandHandler(IApplicationDbContext context, IIdentityApplicationUserService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task<Result> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetUserByUsernameAsync(request.Email);

        if (entity != null)
        {
            return new Result(false, new string[] { "ApplicationUser Already Exists" }, null);
        }

        ApplicationUser applicationUser = new ApplicationUser
        {
            FullNameAR = request.Name,
            FullNameEN = request.Name,
            Email = request.Email,
        };
    
        _context.ApplicationUsers.Add(applicationUser);

        var contributorId = await _context.SaveChangesAsync(cancellationToken);

        int userLogin = await _identityService.CreateUserLoginAsync(applicationUser, request.Password);

        if(userLogin > 0)
        {
            return Result.Success(userLogin);
        }

        return Result.Success(contributorId);
    }
}
