using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Constants;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.ApplicationUsers.Commands.CreateApplicationUser;

public class CreateApplicationUserCommandValidator : AbstractValidator<CreateApplicationUserCommand>
{
    private readonly IIdentityApplicationUserService _identityService;

    public CreateApplicationUserCommandValidator(IIdentityApplicationUserService identityService)
    {
        _identityService = identityService;

        RuleFor(v => v.Name)
            .MaximumLength(100).WithMessage(DefaultValidationMessageConsts.MaxLengthErrorMsg)
            .NotEmpty();


        RuleFor(v => v.Password)
            .MaximumLength(30).WithMessage(DefaultValidationMessageConsts.MaxLengthErrorMsg)
            .MinimumLength(6).WithMessage(DefaultValidationMessageConsts.MinLengthErrorMsg)
            .NotEmpty();

        RuleFor(v => v.Email)
            .MaximumLength(DefaultFieldLengthConsts.MaxEmailLength)
            .EmailAddress().WithMessage(DefaultValidationMessageConsts.BeEmailErrorMsg)
            .NotEmpty().MustAsync(BeUnique).WithMessage(DefaultValidationMessageConsts.BeUniqueErrorMsg)
            .WithErrorCode(ErrorCodesConsts.UniqueErrorCode);
    }

    public async Task<bool> BeUnique(string email, CancellationToken cancellationToken)
    {
        return !await _identityService.EmailExists(email);
    }
}
