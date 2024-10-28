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
public class LoginCommand : IRequest<LoginResponseDto>
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
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

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.AuthenticateAsync(request.Email, request.Password);

        Guard.Against.NotFound(request.Email, result);

        if (!result.Result.Succeeded)
        {
            return new LoginResponseDto { Succeeded = false, Errors = result.Result.Errors };
        }

        var contributor = await _context.Contributors.SingleOrDefaultAsync(x => x.EmailAddress == request.Email);

        if (contributor == null) {
            return new LoginResponseDto { Succeeded = false, Errors = new String[] { "Contributor doesn't exist" } };
        }

        ApplicationUserDto user = new()
        {
            NameEN = contributor.FullNameEN,
            NameAR = contributor.FullNameAR,
            Email = contributor.EmailAddress
        };

        return new LoginResponseDto
        {
            User = user,
            Token = _tokenService.CreateJwtSecurityToken(result.Id.ToString(), "Administrator")
        };
    }
}
