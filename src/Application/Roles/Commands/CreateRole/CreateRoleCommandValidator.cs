namespace SyriacSources.Backend.Application.Roles.Commands.CreateRole;

public class CreateUserCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.NameEN)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(v => v.NameAR)
            .MaximumLength(200)
            .NotEmpty();
    }
} 
