
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Permissions.Commands.UpdatePermission;

public class UpdateRolePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
{
       private readonly IApplicationDbContext _context;

    public UpdateRolePermissionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

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
        return await _context.Permissions
            .AllAsync(l => l.PermissionName != name, cancellationToken);
    }

}
