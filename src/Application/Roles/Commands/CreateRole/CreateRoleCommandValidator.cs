namespace SyriacSources.Backend.Application.Roles.Commands.CreateRole;

public class CreateUserCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(v => v.Description)
            .MaximumLength(200)
            .NotEmpty();
    }
}
