using SyriacSources.Backend.Application.Permissions.Queries.GetPermissions;

namespace SyriacSources.Backend.Application.Permissions.Queries.GetPermissions;

public class GetRolePermissionsQueryValidator : AbstractValidator<GetPermissionsQuery>
{
    public GetRolePermissionsQueryValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required.");

    }
}
