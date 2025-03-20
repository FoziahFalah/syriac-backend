
using SyriacSources.Backend.Application.Common.Constants;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Permissions.Commands.UpdatePermission;

public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
{
       private readonly IApplicationDbContext _context;

    public UpdatePermissionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.NameAR)
            .NotEmpty()
            .MaximumLength(DefaultFieldLengthConsts.MaxNameLength);

        RuleFor(v => v.Description)
            .MaximumLength(200);

    }

    public async Task<bool> BeUnique(string name, CancellationToken cancellationToken)
    {
        return await _context.ApplicationPermissions
            .AllAsync(l => l.PolicyName != name, cancellationToken);
    }

}
