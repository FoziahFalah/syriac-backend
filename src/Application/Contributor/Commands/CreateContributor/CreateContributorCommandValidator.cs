using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Contributors.Commands.CreateContributor;

public class CreateContributorCommandValidator : AbstractValidator<CreateContributorCommand>
{
    private readonly IIdentityApplicationUserService _identityService;

    public CreateContributorCommandValidator(IIdentityApplicationUserService identityService)
    {
        _identityService = identityService;

        RuleFor(v => v.Name)
            .MaximumLength(100)
            .NotEmpty();


        RuleFor(v => v.Password)
            .MaximumLength(30)
            .MinimumLength(6).WithMessage("Please insert atleast {0} characters")
            .NotEmpty();

        RuleFor(v => v.Email)
            .MaximumLength(200)
            .NotEmpty().MustAsync(BeUnique)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUnique(string email, CancellationToken cancellationToken)
    {
        return await _identityService.EmailExists(email);
    }
}
