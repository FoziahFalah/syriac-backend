using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Permissions.Commands.CreatePermission;
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

    //https://github.com/iayti/CleanArchitecture/blob/master/src/Common/CleanArchitecture.Infrastructure/Identity/IdentityService.cs#L36
    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.AuthenticateAsync(request.Email, request.Password);

        Guard.Against.NotFound(request.Email, result);
        if (!result.Result.Succeeded) 
        {

            return new LoginResponseDto { Succeeded = false, Errors = result.Result.Errors };
        }

        return new LoginResponseDto
        {
            User = result.User,
            Token = _tokenService.CreateJwtSecurityToken(result.User?.Id.ToString())
        };
    }
}
