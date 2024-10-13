using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<string>
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.CreateUserAsync(request.Name , request.Password);

        return user.UserId;
    }
}
