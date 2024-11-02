using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Permissions.Commands.CreatePermission;
using SyriacSources.Backend.Application.User;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Account.Commands.Login;
public class LoginCommand : IRequest<LoginResponseVm>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseVm>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;
    private readonly IApplicationDbContext _context;


    public LoginCommandHandler(IApplicationDbContext context, ITokenService tokenService, IIdentityService identityService)
    {
        _identityService = identityService;
        _tokenService = tokenService;
        _context = context;
    }

    public async Task<LoginResponseVm> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //var result = await _identityService.AuthenticateAsync(request.Email, request.Password);
        var user = await _identityService.GetUserAsync(request.Email);

        if (user == null)
        {
            return new LoginResponseVm { Succeeded = false, Errors = new String[]{ "Username or Password is incorrect"} };
        }

        var contributor = await _context.Contributors.SingleOrDefaultAsync(x => x.EmailAddress == request.Email);

        if (contributor == null) {
            return new LoginResponseVm { Succeeded = false, Errors = new String[] { "User is not authorized" } };
        }

        UserBasicDetailsVm details = new UserBasicDetailsVm { 
            Email = request.Email,
            Id = user.Id,
            Name = contributor.FullNameEN
        };

        return new LoginResponseVm
        {
            UserBasicDetails = details,
            Token = _tokenService.CreateJwtSecurityToken(user.Id.ToString(), "Administrator")
        };
    }
}
