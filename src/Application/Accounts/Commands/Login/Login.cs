using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.User;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Accounts.Commands.Login;
public class LoginCommand : IRequest<LoginResponseVm>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseVm>
{
    private readonly ITokenService _tokenService;
    private readonly IIdentityApplicationUserService _identityService;
    private readonly IApplicationDbContext _context;


    public LoginCommandHandler(IApplicationDbContext context, ITokenService tokenService, IIdentityApplicationUserService identityService)
    {
        _tokenService = tokenService;
        _identityService = identityService;
        _context = context;
    }

    public async Task<LoginResponseVm> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        LoginResponseVm loginResponse = new LoginResponseVm();

        //Identity User Authentication
        var user = await _identityService.AuthenticateAsync(request.Email, request.Password); //will return he user

        if (!user.result.Succeeded)
        {
            return new LoginResponseVm { Succeeded = false, Errors = user.result.Errors };
        }

        var appUser = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.Email == request.Email);

        if (appUser == null) {
            return new LoginResponseVm { Succeeded = false, Errors = new String[] { "User is not authorized" } };
        }

        UserBasicDetailsVm details = new UserBasicDetailsVm { 
            Id = user.id, //IdentityId
            Email = appUser.Email,
            UserName = appUser.UserName,
            Name = appUser.FullNameEN
        };

        List<ApplicationUserRole>? applicationUserRole = await _context.ApplicationUserRoles
            .Where(x => x.ApplicationUserId == appUser.Id)
            .Include(x=>x.ApplicationRole)
            .ToListAsync(cancellationToken);
        if(applicationUserRole == null)
        {
            details.Roles = "";
        }

        details.Roles = string.Join(",", applicationUserRole?.Select(x=>x.ApplicationRole?.Id.ToString()).ToList() ?? new List<string?>());

        return loginResponse = new LoginResponseVm
        {
            UserBasicDetails = details,
            Token = await _tokenService.CreateJwtSecurityToken(details)
        };
    }
}
