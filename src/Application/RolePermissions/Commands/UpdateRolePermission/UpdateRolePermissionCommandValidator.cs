
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.RolePermissions.Commands.UpdateRolePermission;

public class UpdateRolePermissionCommandValidator : AbstractValidator<UpdateRolePermissionCommand>
{
    public UpdateRolePermissionCommandValidator(IApplicationDbContext context)
    {

    }
}
