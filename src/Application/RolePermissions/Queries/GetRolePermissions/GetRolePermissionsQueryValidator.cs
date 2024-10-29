
namespace SyriacSources.Backend.Application.RolePermissions.Queries.GetRolePermissions;

public class GetRolePermissionsQueryValidator : AbstractValidator<GetRolePermissionsQuery>
{
    public GetRolePermissionsQueryValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required.");

    }
}
