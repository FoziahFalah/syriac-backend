
using SyriacSources.Backend.Application.Common.Extensions;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Permissions.Commands.UpdatePermission;

public class UpdateApplicationUserCommandValidator : AbstractValidator<UpdatePermissionCommand>
{
       private readonly IApplicationDbContext _context;
       private readonly IApplicationRoleService _roleManager;

    public UpdateApplicationUserCommandValidator(IApplicationDbContext context, IApplicationRoleService roleManager)
    {
        _context = context;
        _roleManager = roleManager;

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(100)
            .MustAsync(BeUnique)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");

        RuleFor(v => v.Description)
            .MaximumLength(200)
            .NotEmpty();

    }

    public async Task<bool> BeUnique(string name, CancellationToken cancellationToken)
    {
        return (await _roleManager.GetRolesAsync(cancellationToken)).All(l => l.NormalizedRoleName!= name.NormalizeString());
    }

}
