
using SyriacSources.Backend.Application.Common.Extensions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.ApplicationUsers.Commands.UpdateApplicationUser;

namespace SyriacSources.Backend.Application.ApplicationUsers.Commands.UpdateApplicationUser;

public class UpdateApplicationUserCommandValidator : AbstractValidator<UpdateApplicationUserCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateApplicationUserCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.NameAR)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(v => v.NameEN)
            .MaximumLength(100);

    }

}
