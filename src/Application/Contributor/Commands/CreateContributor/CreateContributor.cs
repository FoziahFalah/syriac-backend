using System.Xml.Linq;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Contributors.Commands.CreateContributor;

public record CreateContributorCommand : IRequest<Result>
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; } = "Aa@123456";
}

public class CreateContributorCommandHandler : IRequestHandler<CreateContributorCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityApplicationUserService _identityService;

    public CreateContributorCommandHandler(IApplicationDbContext context, IIdentityApplicationUserService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task<Result> Handle(CreateContributorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetUserAsync(request.Email);

        if (entity != null)
        {
            return new Result(false, new string[] { "Contributor Already Exists" }, null);
        }

        int user = await _identityService.CreateUserAsync(request.Name , request.Password);

        if(user > 0)
        {
            return Result.Success(user);
        }


        ApplicationUser contributor = new ApplicationUser
        {
            FullNameAR = request.Name,
            FullNameEN = request.Name,
            EmailAddress = request.Email
        };
    
        _context.Contributors.Add(contributor);

        var contributorId = await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(contributorId);
    }
}
