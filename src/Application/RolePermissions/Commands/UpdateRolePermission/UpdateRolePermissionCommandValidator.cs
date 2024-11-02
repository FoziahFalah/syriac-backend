using SyriacSources.Backend.Application.ApplicationRolePermissions.Commands.UpdateRolePermission;

namespace SyriacSources.Backend.Application.RolePermissions.Commands.UpdateRolePermission;

public class UpdateRolePermissionCommandValidator : AbstractValidator<UpdateRolePermissionCommand>
{
    public UpdateRolePermissionCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required.");
    }
}
